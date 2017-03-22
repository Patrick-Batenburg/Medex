using System;
using System.Collections.Generic;
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
using WindowsPhoneApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WindowsPhoneApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTaskPage : Page
    {       
        private App app = (Application.Current as App);
        private TaskViewModel taskViewModel = null;
        private decimal costsValue = 0;
        //private bool isTitle = false;
        //private bool isDescription = false;
        //private bool isCostsDecimal = false;
        //private bool[] isValids;
        private TaskViewModel passedData = null;

        public EditTaskPage()
        {
            this.InitializeComponent();
            taskViewModel = new TaskViewModel();
            passedData = new TaskViewModel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
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
        
        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {

        }

        private void DurationTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {

        }

        private void CostsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void RemarksTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

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
                EditTask();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

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
                passedData.Title = TitleTextBox.Text;
                passedData.Date = DatePicker.Date.DateTime.ToString();
                passedData.Duration = DurationTimePicker.Time.ToString();
                passedData.Description = DescriptionTextBox.Text;
                passedData.Remarks = RemarksTextBox.Text;
                passedData.Costs = costsValue;

                if (result == true)
                {
                    app.DisplayMessageBox("Taak is gewijzigt.");
                    Frame.Navigate(typeof(StartPage));
                }
                else
                {
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
