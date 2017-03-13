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
using WindowsPhoneApp.Database;
using WindowsPhoneApp.Database.Models;
using System.Text.RegularExpressions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        DatabaseHandler database = new DatabaseHandler();
        bool isEmail;

        public RegisterPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isEmail = Regex.IsMatch(EmailTextBox.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            switch (isEmail)
            {
                case true:
                    switch (UsernameTextBox.Text)
                    {
                        case "":
                            DisplayMessageBox("Gebruikersnaam is ongeldig.");
                            break;
                        default:
                            List<Users> users = database.ReadUsers();

                            if (users.Capacity > 0)
                            {
                                foreach (Users user in users)
                                {
                                    if (UsernameTextBox.Text == user.Username)
                                    {
                                        DisplayMessageBox("Gebruikersnaam is al in gebruik.");
                                    }
                                    else
                                    {
                                        if (UsernameTextBox.Text.Count() > 2)
                                        {
                                            switch (PasswordBox.Password)
                                            {
                                                case "":
                                                    DisplayMessageBox("Wachtwoord is ongeldig.");
                                                    break;
                                                default:
                                                    if (PasswordBox.Password == RepeatPasswordBox.Password)
                                                    {
                                                        if (RepeatPasswordBox.Password.Count() > 4)
                                                        {
                                                            try
                                                            {
                                                                database.AddUser(new Users { Username = UsernameTextBox.Text, Email = EmailTextBox.Text, Password = PasswordBox.Password });
                                                                DisplayMessageBox("Account is succesvol geregistreerd.");
                                                                Frame.Navigate(typeof(MainPage));

                                                            }
                                                            catch
                                                            {
                                                                DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            DisplayMessageBox("Wachtwoord is te kort.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        DisplayMessageBox("Wachtwoord komt niet overeen.");
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            DisplayMessageBox("Gebruikersnaam is te kort.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (UsernameTextBox.Text.Count() > 2)
                                {
                                    switch (PasswordBox.Password)
                                    {
                                        case "":
                                            DisplayMessageBox("Wachtwoord is ongeldig.");
                                            break;
                                        default:
                                            if (PasswordBox.Password == RepeatPasswordBox.Password)
                                            {
                                                if (RepeatPasswordBox.Password.Count() > 4)
                                                {
                                                    try
                                                    {
                                                        database.AddUser(new Users { Username = UsernameTextBox.Text, Email = EmailTextBox.Text, Password = PasswordBox.Password });
                                                        DisplayMessageBox("Account is succesvol geregistreerd.");
                                                        Frame.Navigate(typeof(MainPage));
                                                    }
                                                    catch
                                                    {
                                                        DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
                                                    }
                                                }
                                                else
                                                {
                                                    DisplayMessageBox("Wachtwoord is te kort.");
                                                }
                                            }
                                            else
                                            {
                                                DisplayMessageBox("Wachtwoord komt niet overeen.");
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    DisplayMessageBox("Gebruikersnaam is te kort.");
                                }
                            }
                            break;
                    }
                    break;
                case false:
                    DisplayMessageBox("Ingevulde email is ongeldig.");
                    break;
                default:
                    break;
           }
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void DisplayMessageBox(string message)
        {
            MessageDialog msgBox = new MessageDialog(message);
            await msgBox.ShowAsync();
        } 
    }
}
