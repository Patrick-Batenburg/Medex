using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WindowsPhoneApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private App app = (Application.Current as App);
        private TaskViewModel taskViewModel = null;

        public StartPage()
        {
            this.InitializeComponent();
            taskViewModel = new TaskViewModel();
            TaskListView.ItemsSource = taskViewModel.GetTasks();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            app.DisplayMessageBox("Uw bent zojuist uit gelogd.");
            Frame.Navigate(typeof(MainPage));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTaskPage));
        }

        private void TaskListView_ItemClicked(object sender, ItemClickEventArgs e)
        {
            //Frame.Navigate(typeof(ViewTaskPage), e.ClickedItem);
        }
    }
}