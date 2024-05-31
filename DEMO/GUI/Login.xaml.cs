using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using BLL;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }
        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        


        private void openAdminForm()
        {
            // Mở giao diện admin
            MainWindow f = new MainWindow();
            f.Show();
            Window.GetWindow(this).Close();
        }

        private void openUserForm()
        {
            // Mở giao diện người dùng thông thường
            StaffWindow f = new StaffWindow();
            f.Show();
            Window.GetWindow(this).Close();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ACCOUNT acc = new ACCOUNT();
            //ACCOUNT_BLL accBLL = new ACCOUNT_BLL();
            //acc.Email = txtEmail.Text;
            //acc.PasswordUser = txtPassword.Password;

            //string getuser = accBLL.CheckLogic(acc);

            // Thể hiện trả lại kết quả nếu nghiệp vụ không đúng
            /* switch (getuser)
             {
                 case "required tk_email":
                     MessageBox.Show("Tài khoản không được để trống");
                     return;

                 case "required pass":
                     MessageBox.Show("Mật khẩu không được để trống");
                     return;

                 case "Tai khoan hoac mat khau khong chinh xac!":
                     MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
                     return;
             }
            */
            /*
             string getPermission = accBLL.GetPermission(acc);
             */


            string email = txtEmail.Text;
            string password = txtPassword.Password;

            ACCOUNT_BLL accountBLL = new ACCOUNT_BLL();
            int permissionID;

            if (accountBLL.AuthenticateAccount(email, password, out permissionID))
            {
                // Tài khoản và mật khẩu đúng
                switch (permissionID)
                {
                    case 1:
                        // Xử lý khi permissionID = 1 (Ví dụ: mở giao diện admin)
                        openAdminForm();
                        break;
                    case 2:
                        // Xử lý khi permissionID = 2 (Ví dụ: mở giao diện người dùng thông thường)
                        openUserForm();
                        break;
                    default:
                        MessageBox.Show("Quyền không hợp lệ");
                        break;
                }
            }
            else
            {
                // Tài khoản hoặc mật khẩu không đúng
                MessageBox.Show("Tài khoản không hợp lệ");
            }



            /*MessageBox.Show("sign in suceess");
            MainWindow f = new MainWindow();
            f.Show();
            Window.GetWindow(this).Close();*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignUp1 f = new SignUp1();
            f.Show();
            Window.GetWindow(this).Close();
        }
    }
}
