using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : UserControl
    {
        private ICollectionView collectionView;

        public Window4()
        {
            InitializeComponent();

            ObservableCollection<TicketClass> ticketList = new ObservableCollection<TicketClass>
            {
                new TicketClass { ID = "1", Name = "Standard", Quantity = 70 },
                new TicketClass { ID = "2", Name = "Premium1", Quantity = 20 },
                new TicketClass { ID = "3", Name = "Premium2", Quantity = 20 },
                new TicketClass { ID = "4", Name = "VIP1", Quantity = 10 },
                new TicketClass { ID = "5", Name = "VIP2", Quantity = 5 },
                new TicketClass { ID = "6", Name = "Elon Musk", Quantity = 1 }

            };
            collectionView = CollectionViewSource.GetDefaultView(ticketList);
            dataGrid.ItemsSource = collectionView;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            TicketClass selectedTicket = (TicketClass)dataGrid.SelectedItem;
            if (selectedTicket != null)
            {
                ((ObservableCollection<TicketClass>)collectionView.SourceCollection).Remove(selectedTicket);
                dataGrid.ItemsSource = collectionView;
            }
        }
    }

    public class TicketClass
    {
        public string ID { get; set; }
        public string Name { get; set;}
        public int Quantity { get; set; }
    }
}
