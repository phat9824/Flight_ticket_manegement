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
            var startUpWindow = new Login();
            //UserSession.Instance.StartSession("test", new[] { "test" }.ToList());
            //var startUpWindow = new MainWindow();
            //var startUpWindow = new StaffWindow();
            startUpWindow.Show();
        }
    }


    // Không biết đặt ở đâu nên đặt ở đây
    public class ClientSession
    {
        private static ClientSession instance;

        public string mail { get; private set; }

        public List<string> permissions { get; private set; }

        private ClientSession() { }

        public static ClientSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientSession();
                }
                return instance;
            }
        }

        public void StartSession(string mail, List<string> permissions)
        {
            this.mail = mail;
            this.permissions = permissions;
        }

        public void EndSession()
        {
            this.mail = null;
            this.permissions = null;
        }
    }
}
