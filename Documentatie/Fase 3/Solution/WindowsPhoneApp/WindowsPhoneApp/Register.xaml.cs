using System;
using System.Collections.Generic;
using System.IO;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private string registerName;
        private string registerEmail;
        private string registerPassword;
        public Register()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == txtPasswordRepeat.Text && txtPassword.Text != "")
            {
                registerName = txtName.Text;
                registerEmail = txtEmail.Text;
                registerPassword = txtPassword.Text;
            }
            else
            {
                MessageBoxDisplay_Click(sender, e);
            }
        }
        private async void MessageBoxDisplay_Click(object sender, RoutedEventArgs e)
        {
            //Creating instance for the MessageDialog Class  
            //and passing the message in it's Constructor  
            MessageDialog msgbox = new MessageDialog("The password does not match");
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
        }  

    }
}
