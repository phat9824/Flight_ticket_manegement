﻿using BLL;
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
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SignUp1.xaml
    /// </summary>
    public partial class SignUp1 : Window
    {
        NguoiDungBLL ndbll = new NguoiDungBLL();
        NguoiDung User = new NguoiDung();
        public SignUp1()
        {
            InitializeComponent();
            //Day
            List<int> days = new List<int>();
            for (int i = 1; i <= 31; i++)
            {
                days.Add(i);
            }
            D_comboBox.ItemsSource = days;
            //Month
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }

            M_comboBox.ItemsSource = months;
            //Year
            int currentYear = DateTime.Now.Year;

            // Khởi tạo danh sách các năm
            List<int> years = new List<int>();
            for (int i = currentYear - 120; i <= currentYear + 50; i++) 
            {
                years.Add(i);
            }
            Y_comboBox.ItemsSource = years;
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

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Login f = new Login();
            f.Show();
            Window.GetWindow(this).Close();
        }

        private void SignU_Button_Click(object sender, RoutedEventArgs e)
        {
            User.TenNguoiDung = "asdfasdf";
            User.Email = txtMailAdd.Text.Trim();
            User.NgaySinh = new DateTime((int)Y_comboBox.SelectedValue, (int)M_comboBox.SelectedValue, (int)D_comboBox.SelectedValue);
            User.PasswordND = txtRePassword.Password.Trim();

            int kq = 0;
            ndbll.SignUp(User, ref kq);

            if (kq > 0)
            {
                MessageBox.Show("Sign Up Success");
                Login f = new Login();
                f.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Admin_bt_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
