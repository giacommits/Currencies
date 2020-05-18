using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFCuerrenciesUI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        //Some code behind for front end exclusive functions and responsiveness

        //Disables the spacebar input
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        //Allow only numbers and one '.' character for decimals.
        private void TextBox_PreviewTextInput(object sender,TextCompositionEventArgs e)
        {           
            
            if (IsNumber(e.Text) == true)
            {
                e.Handled = false;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                 }
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool IsNumber(string text)
        {
            int output;
            return int.TryParse(text, out output);
        }

        //Only react to user made changes in textboxes to prevent ininite recursion due to automatics changes in textboxes
        //made by the calculation methods.
        private void TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            if (!((TextBox)sender).IsFocused)
                e.Handled = true;
        }

        //Validation for input in DatePicker
        private void Date_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
                {
                    if (DateTime.Parse(Date.Text) > Date.DisplayDateEnd || DateTime.Parse(Date.Text) < Date.DisplayDateStart)
                    {
                        MessageBox.Show("No registers for such date", "Error");
                        Date.Text = Date.SelectedDate.ToString();
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Invalid date", "Error");
                Date.Text = Date.SelectedDate.ToString();
            }
        }
    }
}
