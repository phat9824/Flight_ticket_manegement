using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using BLL;
using DTO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Đổi start up ở đây
            //var startUpWindow = new Login();
            //var startUpWindow = new MainWindow();
            var startUpWindow = new StaffWindow();
            startUpWindow.Show();
        }
    }
}
