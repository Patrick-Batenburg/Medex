﻿using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Medex.Models;
using Medex.ViewModels;

namespace Medex.Views
{
    /// <summary>
    /// The page where you can add a new task
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
            TitleTextBox.KeyDown += app.OnKeyDown;
            CostsTextBox.KeyDown += app.OnKeyDown;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        
        //Checks if the text is valid as a title
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

        //checks if the text is valid as a description
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

        //checks if the text is valid as costs
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

        //make a control if the task is valid to be added
        private void SafeButton_Click(object sender, RoutedEventArgs e)
        {
            //checks if any of the bools are incorrect
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
                //if there are no flaws found, then it'll add the task successfully
                AddTask();
            }
        }

        //cancels the action and returns to main menu
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        //adds the new task
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
            catch (Exception ex)
            {
                //in case the application run into a problem, it'll cancel 
                app.DisplayMessageBox(string.Format("Er is een onbekent probleem opgetreden, probeer het later opnieuw./nfoutcode: {0} ", ex.ToString()));
            }
        }
    }
}
