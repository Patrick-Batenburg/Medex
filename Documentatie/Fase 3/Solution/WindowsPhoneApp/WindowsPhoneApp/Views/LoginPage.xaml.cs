using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.Providers;
using Medex.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Medex.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private App app = (Application.Current as App);
        private UserViewModel userViewModel = null;
        private UserViewModel result = null;
        private EncryptionProvider encryptionProvider = null;

        public LoginPage()
        {
            this.InitializeComponent();
            result = new UserViewModel();
            userViewModel = new UserViewModel();
            encryptionProvider = new EncryptionProvider();
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
            result = userViewModel.GetUser(UsernameTextBox.Text, encryptionProvider.Encrypt(PasswordBox.Password));

            if (result != null)
            {
                app.CURRENT_USER_ID = result.Id;
                app.DisplayMessageBox("Welkom " + result.Username);
                this.Frame.Navigate(typeof(StartPage));
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