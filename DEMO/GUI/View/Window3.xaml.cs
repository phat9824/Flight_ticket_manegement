using GUI.ViewModel;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : UserControl
    {
        ObservableCollection<Members> members = new ObservableCollection<Members>();
        private List<string> suggestions = new List<string> { "Gợi ý 1", "Gợi ý 2", "Gợi ý 3" };
        public Window3()
        {
            InitializeComponent();
        }

        public class Members
        {
            public string Character { get; set; }
            public Brush BgColor { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}
