using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Security;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Providers;
using WindowsPhoneApp.ViewModels;
using System.Collections.ObjectModel;
using WindowsPhoneApp.Models;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private bool[] isValids;
        private bool isUsername = false;
        private bool isEmail = false;
        private bool isPassword = false;
        private UserViewModel userViewModel = null;
        private List<UserViewModel> users = null;
        private App app = (Application.Current as App);

        public RegisterPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
            users = new List<UserViewModel>();
            isValids = new bool[] {isUsername, isEmail, isPassword};
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
			if (UsernameTextBox.Text.Count() > 2)
            {
                isUsername = true;
            }
            else
            {
                isUsername = false;
            }
            TextboxCorrection(UsernameTextBox, isUsername);
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isEmail = Regex.IsMatch(EmailTextBox.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            TextboxCorrection(EmailTextBox, isEmail);
        }
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
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //bool result = false;
            isValids = new bool[] { isUsername, isEmail, isPassword };
            if (isValids.Contains<bool>(false))
            {
                //search what cause the problem
                if (isEmail == false)
                {
                    app.DisplayMessageBox("Ingevulde Email is ongeldig");
                }
                else if (isUsername == false)
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
                        app.DisplayMessageBox("Gebruikersnaam is ongeldig");
                    }
                }
                else if (isPassword == false)
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
                //register
                users = userViewModel.GetUsers();
                if (users.Count > 0)
                {
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

        private void RegisterUser()
        {
            bool result = false;
            try
            {
                result = userViewModel.AddUser(new User() 
                { 
                    Email = EmailTextBox.Text.ToLower(), 
                    Username = UsernameTextBox.Text, 
                    Password = EncryptionProvider.Encrypt(RepeatPasswordBox.Password) 
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

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}