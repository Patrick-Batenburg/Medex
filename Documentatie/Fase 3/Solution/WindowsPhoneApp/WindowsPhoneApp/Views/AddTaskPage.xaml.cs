using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
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
    public sealed partial class AddTaskPage : Page
    {
        private App app = (Application.Current as App);
        private TaskViewModel taskViewModel = null;
        private decimal costsValue = 0;
        private bool isTitle = false;
        private bool isDescription = false;
        private bool isCostsDecimal = false;
        private bool[] isValids;

        public AddTaskPage()
        {
            this.InitializeComponent();
            taskViewModel = new TaskViewModel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        
        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TitleTextBox.Text != string.Empty)
            {
                isTitle = true;
            }
            else
            {
                isTitle = false;
            }
        }

        private void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DescriptionTextBox.Text != string.Empty)
            {
                isDescription = true;
            }
            else
            {
                isDescription = false;
            }
        }

        private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {

        }

        private void DurationTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {

        }

        private void CostsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Decimal.TryParse(CostsTextBox.Text, out costsValue))
            {
                CostsTextBox.Text = Math.Round(costsValue, 2).ToString();
                isCostsDecimal = true;
            }
            else
            {
                isCostsDecimal = false;
            }
        }

        private void RemarksTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SafeButton_Click(object sender, RoutedEventArgs e)
        {
            isValids = new bool[] { isTitle, isDescription, isCostsDecimal };
            if (isValids.Contains<bool>(false))
            {
                //search what cause the problem
                if (isTitle == false)
                {
                    app.DisplayMessageBox("Titel is verplicht");
                }
                else if (isDescription == false)
                {
                    app.DisplayMessageBox("Omschrijving is verplicht");
                }
                else if (isCostsDecimal == false)
                {
                    app.DisplayMessageBox("Kosten is ongeldig.");
                }
            }
            else
            {
                AddTask();
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

        private void AddTask()
        {
            bool result = false;

            try 
            {
                result = taskViewModel.AddTask(new Task()
                {
                    Title = TitleTextBox.Text,
                    Date = DatePicker.Date.DateTime,
                    Duration = DurationTimePicker.Time,
                    Description = DescriptionTextBox.Text,
                    Remarks = RemarksTextBox.Text,
                    Costs = costsValue
                });

                if (result == true)
                {
                    app.DisplayMessageBox("Taak is toegevoegd.");
                    Frame.Navigate(typeof(StartPage));
                }
            }
            catch
            {
                app.DisplayMessageBox("Er is een onbekent probleem opgetreden, probeer het later opnieuw.");
            }
        }
    }
}
