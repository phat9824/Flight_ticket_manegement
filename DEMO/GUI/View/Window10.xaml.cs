using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.View
{
    public partial class Window10 : UserControl
    {
        private string connectionString = "Data Source=<your_database_server>;Initial Catalog=<your_database_name>;User ID=<your_username>;Password=<your_password>";

        public Window10()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
