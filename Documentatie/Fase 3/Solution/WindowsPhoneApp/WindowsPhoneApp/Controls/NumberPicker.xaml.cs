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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WindowsPhoneApp.Controls
{
    public sealed partial class NumberPicker : UserControl
    {
        bool enableDecimals = false;

        public NumberPicker()
        {
            this.InitializeComponent();
        }

        public NumberPicker(bool enableDecimals)
        {
            this.InitializeComponent();
            this.enableDecimals = enableDecimals;
        }

        private void Reduce_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (enableDecimals == false)
            {
                var currentValue = int.Parse(content.Text);

                if (currentValue == 0)
                {
                    return;
                }
                content.Text = (currentValue - 1).ToString();
            }
            else
            {
                var currentValue = decimal.Parse(content.Text);

                if (currentValue == 0)
                {
                    return;
                }
                content.Text = (currentValue - 0.01m).ToString();
            }
        }

        private void Edit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            edit.Visibility = Visibility.Visible;
            content.Visibility = Visibility.Collapsed;
            edit.LostFocus += textBox_LostFocus;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (enableDecimals == false)
            {
                if (edit.Text != string.Empty)
                {
                    var currentValue = int.Parse(content.Text);
                    content.Text = (currentValue).ToString();
                }
            }
            else
            {
                if (edit.Text != string.Empty)
                {
                    var currentValue = decimal.Parse(edit.Text);
                    content.Text = (currentValue).ToString();
                }
            }
            edit.Visibility = Visibility.Collapsed;
            content.Visibility = Visibility.Visible;
        }

        private void Increase_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (enableDecimals == false)
            {
                var currentValue = int.Parse(content.Text);
                content.Text = (currentValue + 1).ToString();
            }
            else
            {
                var currentValue = decimal.Parse(content.Text);
                content.Text = (currentValue + 0.01m).ToString();
            }
        }
    }
}
