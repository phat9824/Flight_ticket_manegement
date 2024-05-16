using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window10.xaml
    /// </summary>
    public partial class Window10 : UserControl
    {
        public Window10()
        {
            InitializeComponent();
        }

        private void textN_Airport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtN_Airport.Text) && txtN_Airport.Text.Length > 0)
            {
                textN_Airport.Visibility = Visibility.Collapsed;
            }
            else
            {
                textN_Airport.Visibility = Visibility.Visible;
            }
        }

        private void txtN_Airport_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtN_Airport.Focus();
        }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresentTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker1_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresentTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker2_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresentTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void textN_Ticket_class_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtN_Ticket_class.Text) && txtN_Ticket_class.Text.Length > 0)
            {
                textN_Ticket_class.Visibility = Visibility.Collapsed;
            }
            else
            {
                textN_Ticket_class.Visibility = Visibility.Visible;
            }
        }

        private void txtN_Ticket_class_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtN_Ticket_class.Focus();
        }

        private void PresetTimePicker3_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresentTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker4_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresentTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }
    }
}
