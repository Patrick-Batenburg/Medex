using System.Text.RegularExpressions;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Medex.Providers;
using Medex.ViewModels;
using System.Collections.ObjectModel;
using Medex.Models;
using Windows.UI;

namespace Medex.Views
{
    /// <summary>
    /// The registerpage where you can register a new user
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private bool[] isValids;
        private bool isUsername = false;
        private bool isEmail = false;
        private bool isPassword = false;
        private UserViewModel userViewModel = null;
        private ObservableCollection<UserViewModel> users = null;
        private App app = (Application.Current as App);
        private EncryptionProvider encryptionProvider = null;

        public RegisterPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
            users = new ObservableCollection<UserViewModel>();
            encryptionProvider = new EncryptionProvider();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        //checks if the username is valid
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //^(?=.{3,20}$)(?![_-])[a-zA-Z0-9._]+(?<![_-])$
            // └─────┬────┘└───┬──┘└─────┬─────┘ └────┬───┘
            //       │         │         │            │           
            //       │         │         │            │
            //       │         │         │            no _ or - at the end
            //       │         │         │
            //       │         │         allowed characters
            //       │         │
            //       │         no _ or - at the beginning
            //       │
            //       username is 3-20 characters long
            isUsername = Regex.IsMatch(UsernameTextBox.Text, @"^(?=.{3,20}$)(?![_-])[a-zA-Z0-9-_]+(?<![_-])$", RegexOptions.None);
            TextboxCorrection(UsernameTextBox, isUsername);
        }
        //checks if the email is valid
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isEmail = Regex.IsMatch(EmailTextBox.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            TextboxCorrection(EmailTextBox, isEmail);
        }

        //uses this method as multiple textboxes are using the same code
        private void TextboxCorrection(TextBox textbox, bool isValid)
        {
            if (textbox.Text.Count() == 0)
            {
                textbox.Background = null;
            }
            else
            {
                if (isValid == false)
                {
                    textbox.Background = new SolidColorBrush() { Color = Colors.LightPink };
                }
                else
                {
                    textbox.Background = null;
                }
            }
        }

        //checks if the password is valid
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == RepeatPasswordBox.Password && PasswordBox.Password.Count() > 4)
            {
                isPassword = true;
            }
            else
            {
                isPassword = false;
            }

            //redone the TextboxCorrection() here because a passwordbox is not a textbox
            if (PasswordBox.Password.Count() == 0)
            {
                PasswordBox.Background = null;
            }
            else
            {
                if (isPassword == false)
                {
                    PasswordBox.Background = new SolidColorBrush() { Color = Colors.LightPink };
                }
                else
                {
                    PasswordBox.Background = null;
                }
            }
        }

        //register the new user if the requirements are met
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if it's valid to save the data
            isValids = new bool[] { isUsername, isEmail, isPassword };
            if (isValids.Contains<bool>(false))
            {
                //search if the problem lies on the E-mail
                if (isEmail == false)
                {
                    app.DisplayMessageBox("Ingevulde Email is ongeldig");
                }
                else if (isUsername == false)//checks the username if it's not the E-mail
                {
                    if (UsernameTextBox.Text == string.Empty)
                    {
                        app.DisplayMessageBox("Voer een Gebruikersnaam in.");
                    }
                    else if (UsernameTextBox.Text.Count() <= 2)
                    {
                        app.DisplayMessageBox("Gebruikersnaam is te kort.");
                    }
                    else
                    {
                        app.DisplayMessageBox("Gebruikersnaam is ongeldig. Gebruikernaam mag geen speciale tekens bevatten behalve voor _ en -. Gebruikernaam mag niet beginnen en endigen met _ en -.");
                    }
                }
                else if (isPassword == false) //checks the password if the problem isn't the username either
                {
                    if (UsernameTextBox.Text == string.Empty)
                    {
                        app.DisplayMessageBox("Voer een wachtwoord in.");
                    }

                    if (PasswordBox.Password != RepeatPasswordBox.Password)
                    {
                        app.DisplayMessageBox("Wachtwoord komt niet overeen.");
                    }
                    else if (RepeatPasswordBox.Password.Count() <= 4)
                    {
                        app.DisplayMessageBox("Wachtwoord is te kort.");
                    }
                    else
                    {
                        app.DisplayMessageBox("Er is een probleem opgetreden met het wachtwoord.");
                    }
                }
            }
            else
            {
                //does a final check before registering
                users = userViewModel.GetUsers();
                if (users.Count > 0)
                {
                    //checks if an user in the database has the same name as the new user
                    bool hasSameUsernames = false;
                    foreach (UserViewModel user in users)
                    {
                        if (UsernameTextBox.Text.ToLower() == user.Username.ToLower())
                        {
                            hasSameUsernames = true;
                        }
                    }
                    if (hasSameUsernames == false)
                    {
                        RegisterUser();
                    }
                    else
                    {
                        app.DisplayMessageBox("Gebruikersnaam is al in gebruik.");
                    }
                }
                else
                {
                    RegisterUser();
                }
            }
        }

        //register the new user
        private void RegisterUser()
        {
            bool result = false;

            try
            {
                result = userViewModel.AddUser(new User()
                {
                    Email = EmailTextBox.Text.ToLower(),
                    Username = UsernameTextBox.Text,
                    Password = encryptionProvider.Encrypt(RepeatPasswordBox.Password)
                });
                if (result == true)
                {
                    app.DisplayMessageBox("Registratie is succesvol.");
                    Frame.Navigate(typeof(MainPage));
                }
            }
            catch
            {
                app.DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
            }
        }
    }
}