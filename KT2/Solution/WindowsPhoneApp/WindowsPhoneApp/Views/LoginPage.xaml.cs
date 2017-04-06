using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.Providers;
using Medex.ViewModels;

namespace Medex.Views
{
    /// <summary>
    /// The page where you can login as user
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

            if (app.USERNAME != null && app.PASSWORD != null)
            {
                UsernameTextBox.Text = app.USERNAME;
                PasswordBox.Password = app.PASSWORD;  
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        //checks if the user exist in the database, and if the users' password is correct
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            result = userViewModel.GetUser(UsernameTextBox.Text, encryptionProvider.Encrypt(PasswordBox.Password));

            if (result != null)
            {
                app.CURRENT_USER_ID = result.Id;
                app.DisplayMessageBox("Welkom " + result.Username);
                this.Frame.Navigate(typeof(StartPage));
                app.USERNAME = UsernameTextBox.Text;
                app.PASSWORD = PasswordBox.Password;
            }
            else
            {
                app.DisplayMessageBox("Gebruikersnaam of wachtwoord is onjuist.");
            }
        }
    }
}