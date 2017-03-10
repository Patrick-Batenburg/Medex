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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        DatabaseHandler database = new DatabaseHandler();
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

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == repeatPasswordBox.Password && passwordBox.Password != "")
            {
                List<Users> users = database.ReadUsers();
                bool availableUsername = true;
                foreach(Users user in users)
                {
                    if (usernameTextBox.Text == user.Username)
                    {
                        availableUsername = false;
                    }
                }
                if (availableUsername == true )
                {
                    try
                    {
                        database.AddUser(new Users { Username = usernameTextBox.Text, Email = emailTextBox.Text, Password = passwordBox.Password });
                        DisplayMessageBox("Registreren succesvol.");
                    }
                    catch
                    {
                        DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
                    }
                }
            }
            else
            {
                DisplayMessageBox("Wachtwoord komt niet overeen.");
            }
        }

        private void RepeatPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
        private async void DisplayMessageBox(string message)
        {           
            MessageDialog msgbox = new MessageDialog(message);
            await msgbox.ShowAsync();
        } 
    }
}
