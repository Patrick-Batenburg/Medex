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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private bool isEmail;
        private UserViewModel userViewModel = null;
        private List<User> users = null;
        private App app = (Application.Current as App);

        public RegisterPage()
        {
            this.InitializeComponent();
            userViewModel = new UserViewModel();
            users = new List<User>();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var customer = (UserMetaViewModel)e.Parameter;

            base.OnNavigatedTo(e);
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
            bool result = false;

            switch (isEmail)
            {
                case true:
                    if (UsernameTextBox.Text != string.Empty)
                    {
                        users = userViewModel.GetUsers();

                        if (users.Count > 0)
                        {
                            foreach (User user in users)
                            {
                                if (UsernameTextBox.Text == user.Username)
                                {
                                    app.DisplayMessageBox("Gebruikersnaam is al in gebruik.");
                                }
                                else
                                {
                                    if (UsernameTextBox.Text.Count() > 2)
                                    {
                                        if (RepeatPasswordBox.Password.Count() > 4)
                                        {
                                            if (PasswordBox.Password == RepeatPasswordBox.Password)
                                            {
                                                try
                                                {
                                                    result = userViewModel.AddUser(new User() { Email = EmailTextBox.Text, Username = UsernameTextBox.Text, Password = EncryptionProvider.Encrypt(RepeatPasswordBox.Password) });
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
                                            else
                                            {
                                                app.DisplayMessageBox("Wachtwoord komt niet overeen.");
                                            }
                                        }
                                        else
                                        {
                                            app.DisplayMessageBox("Wachtwoord is te kort.");
                                        }
                                    }
                                    else
                                    {
                                        app.DisplayMessageBox("Gebruikersnaam is te kort.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (UsernameTextBox.Text.Count() > 2)
                            {
                                if (RepeatPasswordBox.Password.Count() > 4)
                                {
                                    if (PasswordBox.Password == RepeatPasswordBox.Password)
                                    {
                                        try
                                        {
                                            result = userViewModel.AddUser(new User() { Email = EmailTextBox.Text, Username = UsernameTextBox.Text, Password = EncryptionProvider.Encrypt(RepeatPasswordBox.Password) });
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
                                    else
                                    {
                                        app.DisplayMessageBox("Wachtwoord komt niet overeen.");
                                    }
                                }
                                else
                                {
                                    app.DisplayMessageBox("Wachtwoord is te kort.");
                                }
                            }
                            else
                            {
                                app.DisplayMessageBox("Gebruikersnaam is te kort.");
                            }
                        }
                    }
                    else
                    {
                        app.DisplayMessageBox("Gebruikersnaam is ongeldig.");
                    }
                    break;
                case false:
                    app.DisplayMessageBox("Ingevulde email is ongeldig.");
                    break;
                default:
                    break;
           }
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
