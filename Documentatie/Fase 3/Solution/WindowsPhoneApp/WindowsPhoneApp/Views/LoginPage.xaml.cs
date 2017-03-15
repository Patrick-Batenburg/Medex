using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhoneApp.Models;
using WindowsPhoneApp.Providers;
using WindowsPhoneApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private App app = (Application.Current as App);
        private UserViewModel userViewModel = null;
        private User result = null;

        public LoginPage()
        {
            this.InitializeComponent();
            result = new User();
            userViewModel = new UserViewModel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            result = userViewModel.GetUser(UsernameTextBox.Text, EncryptionProvider.Encrypt(PasswordBox.Password));

            if (result.Username != null && result.Password != null && result.Email != null)
            {
                app.CurrentUserId = result.Id;
                app.DisplayMessageBox("Welkom " + result.Username);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                app.DisplayMessageBox("Gebruikersnaam of wachtwoord is onjuist.");
            }
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}