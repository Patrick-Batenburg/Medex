using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.Models;
using Medex.ViewModels;

namespace Medex.Views
{
    /// <summary>
    /// The code of the page where you can edit the selected task
    /// </summary>
    public sealed partial class EditTaskPage : Page
    {       
        private App app = (Application.Current as App);
        private TaskViewModel taskViewModel = null;
        private decimal costsValue = 0;
        private TaskViewModel passedData = null;

        public EditTaskPage()
        {
            this.InitializeComponent();
            taskViewModel = new TaskViewModel();
            passedData = new TaskViewModel();
            TitleTextBox.KeyDown += app.OnKeyDown;
            CostsTextBox.KeyDown += app.OnKeyDown;
        }
        //puts the passed data in the page, editing in the textboxes with the data
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedData = (e.Parameter as TaskViewModel);
            TitleTextBox.Text = passedData.Title;
            DescriptionTextBox.Text = passedData.Description;
            RemarksTextBox.Text = passedData.Remarks;
            CostsTextBox.Text = passedData.Costs.ToString();
            DatePicker.Date = Convert.ToDateTime(passedData.Date);
            DurationTimePicker.Time = TimeSpan.Parse(passedData.Duration);
        }

        //Checks if it's valid to save the data
        private void SafeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                app.DisplayMessageBox("Titel is verplicht");
            }
            else if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                app.DisplayMessageBox("Omschrijving is verplicht");
            }
            else if (!Decimal.TryParse(CostsTextBox.Text, out costsValue))
            {
                app.DisplayMessageBox("Kosten is ongeldig.");
            }
            else
            {
                //if it's valid, it's saving them
                EditTask();
            }
        }

        //cancels the current action and returns to main menu
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        //Saves the new data
        private void EditTask()
        {
            bool result = false;
            try
            {
                result = taskViewModel.UpdateTask(new Task()
                {
                    Id = passedData.TaskId,
                    Title = TitleTextBox.Text,
                    Date = DatePicker.Date.DateTime,
                    Duration = DurationTimePicker.Time,
                    Description = DescriptionTextBox.Text,
                    Remarks = RemarksTextBox.Text,
                    Costs = costsValue
                });
                if (result == true)
                {
                    app.DisplayMessageBox("Taak is gewijzigd.");
                    Frame.Navigate(typeof(ViewTaskPage), passedData);
                }
                else
                {
                    //if results didn't turned out well, it's throwing an exception, cancelling the attempt
                    throw new Exception();
                }
            }
            catch
            {
                app.DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
            }
        }
    }
}
