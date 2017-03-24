using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Medex.Views
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
            if (TaskListView.Items.Count > 0)
            {   
                TextNoTaskInList.Visibility = Visibility.Collapsed;
            }    
            else
            {
                TextNoTaskInList.Visibility = Visibility.Visible;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            app.DisplayMessageBox("U bent zojuist uit gelogd.");
            Frame.Navigate(typeof(MainPage));
            Frame.BackStack.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTaskPage));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var itemId = (e.AddedItems[0] as ListViewItem).Name; 
            }
        }

        private void TaskListView_ItemClicked(object sender, ItemClickEventArgs e)
        {
            TaskViewModel clickedItem = (e.ClickedItem as TaskViewModel);
            Frame.Navigate(typeof(ViewTaskPage), clickedItem);
        }
    }
}