using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class Window10 : UserControl
    {
        private string connectionString = "Data Source=<your_database_server>;Initial Catalog=<your_database_name>;User ID=<your_username>;Password=<your_password>";

        public Window10()
        {
            InitializeComponent();
        }

        private void textN_Airport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtN_Airport.Focus();
        }

        private void txtN_Airport_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtN_Airport_GotFocus(object sender, RoutedEventArgs e)
        {
            textN_Airport.Visibility = Visibility.Collapsed;
        }

        private void txtN_Airport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtN_Airport.Text))
            {
                textN_Airport.Visibility = Visibility.Visible;
            }
        }

        private void textN_Ticket_class_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtN_Ticket_class.Focus();
        }

        private void txtN_Ticket_class_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtN_Ticket_class_GotFocus(object sender, RoutedEventArgs e)
        {
            textN_Ticket_class.Visibility = Visibility.Collapsed;
        }

        private void txtN_Ticket_class_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtN_Ticket_class.Text))
            {
                textN_Ticket_class.Visibility = Visibility.Visible;
            }
        }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedEventArgs e) { }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresetTimePicker's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker1_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresetTimePicker1's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker2_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresetTimePicker2's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker3_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresetTimePicker3's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void PresetTimePicker4_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var oldValue = e.OldValue.HasValue ? e.OldValue.Value.ToLongTimeString() : "NULL";
            var newValue = e.NewValue.HasValue ? e.NewValue.Value.ToLongTimeString() : "NULL";

            Debug.WriteLine($"PresetTimePicker4's SelectedTime changed from {oldValue} to {newValue}");
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtN_Airport.Text, out int numberOfAirportsEntered))
            {
                CheckAirportCount(numberOfAirportsEntered);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ.");
            }
        }

        private void CheckAirportCount(int enteredAirportCount)
        {
            int currentAirportCount = GetCurrentAirportCount();
            if (enteredAirportCount != currentAirportCount)
            {
                    MessageBox.Show("Số lượng sân bay nhập vào ít hơn số lượng hiện có. Hãy thêm thêm sân bay hoặc xóa bớt sân bay.");
            }
            else
            {
                MessageBox.Show("Số lượng sân bay nhập vào khớp với số lượng hiện có.");
            }
        }

        private int GetCurrentAirportCount()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM AIRPORT";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                count = (int)command.ExecuteScalar();
            }

            return count;
        }
    }
}
