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

namespace GUI
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            //Day
            List<int> days = new List<int>();
            for (int i = 1; i <= 31; i++)
            {
                days.Add(i);
            }

            // Thiết lập dữ liệu cho ComboBox
            comboBox.ItemsSource = days;
            //Month
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }

            // Thiết lập dữ liệu cho ComboBox
            comboBox1.ItemsSource = months;
            //Year
            // Lấy năm hiện tại
            int currentYear = DateTime.Now.Year;

            // Khởi tạo danh sách các năm
            List<int> years = new List<int>();
            for (int i = currentYear - 120; i <= currentYear + 50; i++) // Lấy 10 năm trước và sau năm hiện tại
            {
                years.Add(i);
            }

            // Thiết lập dữ liệu cho ComboBox
            comboBox2.ItemsSource = years;
        }

        private void textFName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtFName.Focus();
        }

        private void txtFName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFName.Text) && txtFName.Text.Length > 0)
            {
                textFName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textFName.Visibility = Visibility.Visible;
            }
        }

        private void textLName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLName.Focus();
        }

        private void txtLName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLName.Text) && txtLName.Text.Length > 0)
            {
                textLName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textLName.Visibility = Visibility.Visible;
            }
        }

        private void textMailAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtMailAdd.Focus();
        }

        private void txtMailAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMailAdd.Text) && txtMailAdd.Text.Length > 0)
            {
                textMailAdd.Visibility = Visibility.Collapsed;
            }
            else
            {
                textMailAdd.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void textRePassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtRePassword.Focus();
        }

        private void txtRePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRePassword.Password) && txtRePassword.Password.Length > 0)
            {
                textRePassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textRePassword.Visibility = Visibility.Visible;
            }
        }

  

        
    }
}
