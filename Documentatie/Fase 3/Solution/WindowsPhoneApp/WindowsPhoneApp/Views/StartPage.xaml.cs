using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.ViewModels;


namespace Medex.Views
{
    /// <summary>
    /// The main window of the application
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

        //let the user return to the main page, and disallows controls to return without logging in again
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

        //Updates the listview
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var itemId = (e.AddedItems[0] as ListViewItem).Name; 
            }
        }

        //navigate to view the task, also giving a parameter 'e' with it so it can take the values of the task
        private void TaskListView_ItemClicked(object sender, ItemClickEventArgs e)
        {
            TaskViewModel clickedItem = (e.ClickedItem as TaskViewModel);
            Frame.Navigate(typeof(ViewTaskPage), clickedItem);
        }
    }
}