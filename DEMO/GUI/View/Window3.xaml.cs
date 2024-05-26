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
using static GUI.View.Window3;

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

            var converter = new BrushConverter();
            ObservableCollection<Members> members = new ObservableCollection<Members>();

            members.Add(new Members { Seq = "1", ID = "000000",Name = "John Doe", Birth = "00/00/00",Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            members.Add(new Members { Seq = "2", ID = "000000", Name = "Reza Alavi", Birth = "00/00/00", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            members.Add(new Members { Seq = "3", ID = "000000", Name = "Dennis Castillo", Birth = "00/00/00", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            members.Add(new Members { Seq = "4", ID = "000000", Name = "Gabriel Cox", Birth = "00/00/00", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
        }

        public class Members
        {
            public string Seq {  get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Birth {  get; set; }
        }
    }
}
