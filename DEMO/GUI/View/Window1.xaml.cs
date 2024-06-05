
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GUI.ViewModel;
using System.Windows.Controls.Primitives;
using BLL;
using DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : UserControl
    {
        public Window1()
        {
            InitializeComponent();

            // test session
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("Mail: " + ClientSession.Instance.mail + "\nPermissions: " + ClientSession.Instance.permissions[0], "Test Session");
            }));

            BLL.ACCOUNT_BLL prc = new BLL.ACCOUNT_BLL();
            var result = prc.List_acc(new ACCOUNT() { Email = ClientSession.Instance.mail});

            Load(result[0]);

        }

        private void Load(ACCOUNT acc)
        {
            Dictionary<int, string> dr = new Dictionary<int, string>()
            {
                {1, "Admin"},
                {2, "Staff"}
            };
            UserName.Text = acc.UserName;
            Birth.Text = acc.Birth.ToString("dd-MM-yyyy");
            Email.Text = acc.Email;
            Phone.Text = acc.Phone;
            if (acc.PermissonID != 0)
            {
                Role.Text = dr[acc.PermissonID].ToString();
            }
            else
            {
                Role.Text = "Unknown";
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_5(object sender, TextChangedEventArgs e)
        {

        }
    }
}
