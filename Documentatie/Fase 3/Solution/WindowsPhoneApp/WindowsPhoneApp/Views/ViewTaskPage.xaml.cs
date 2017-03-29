using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.ViewModels;


namespace Medex.Views
{
    /// <summary>
    /// The viewtaskpage where you view the task
    /// </summary>
    public sealed partial class ViewTaskPage : Page
    {
        private App app = (Application.Current as App);
        private TaskViewModel taskViewModel = null;
        private TaskViewModel passedData = null;

        public ViewTaskPage()
        {
            this.InitializeComponent();
            taskViewModel = new TaskViewModel();
            passedData = new TaskViewModel();
        }
        //sets the passed over data in the textblocks
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedData = (e.Parameter as TaskViewModel);
            TitlePageTextBlock.Text = passedData.Title;
            TitleValueTextBlock.Text = passedData.Title;
            DateValueTextBlock.Text = passedData.Date;
            DurationValueTextBlock.Text = passedData.Duration;
            DescriptionValueTextBlock.Text = passedData.Description;
            CostsValueTextBlock.Text = "€ " + passedData.Costs;

            if (passedData.Remarks == string.Empty)
            {
                RemarksValueTextBlock.Text = "Geen opmerkingen opgegeven."; 
            }
            else
            {
                RemarksValueTextBlock.Text = passedData.Remarks;
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditTaskPage), passedData);
        }

        //Displays a dialogbox to warn you about erasing the task from the database.
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Weet je het zeker?");
            msg.Commands.Add(new UICommand("Ja"));
            msg.Commands.Add(new UICommand("Nee"));
            var result = await msg.ShowAsync();
            if (result.Label == "Ja")
            {
                //erasing the task once 'Yes' is clicked
                TaskViewModel taskViewModel = new TaskViewModel();
                taskViewModel.DeleteTask(passedData.TaskId);
                app.DisplayMessageBox("Taak is succesvol verwijderd.");
                Frame.Navigate(typeof(StartPage));
            }
        }
    }
}
