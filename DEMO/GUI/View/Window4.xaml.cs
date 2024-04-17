using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ObservableCollection<TicketClass> ticketList;
        private ObservableCollection<IntermediateAirport> IAList; // Intermidate Airport List
        private ICollectionView collectionViewTicketClass;
        private ICollectionView collectionViewIA;

        //private bool isEditing = false;

        public Window4()
        {
            InitializeComponent();

            ticketList = new ObservableCollection<TicketClass>
            {
                new TicketClass { ID = "1", Name = "Standard", Quantity = 70 },
                new TicketClass { ID = "2", Name = "Premium1", Quantity = 20 },
                new TicketClass { ID = "3", Name = "Premium2", Quantity = 20 },
                new TicketClass { ID = "4", Name = "VIP1", Quantity = 10 },
                new TicketClass { ID = "5", Name = "VIP2", Quantity = 5 },
                new TicketClass { ID = "6", Name = "Elon Musk", Quantity = 1 }
            };
            collectionViewTicketClass = CollectionViewSource.GetDefaultView(ticketList);
            dataGrid1.ItemsSource = collectionViewTicketClass;

            IAList = new ObservableCollection<IntermediateAirport>
            {
                new IntermediateAirport { ID = "1", Name = "Standard", LayoverTime = TimeSpan.FromMinutes(15), Note = "ahdjbsad" },
                new IntermediateAirport { ID = "2", Name = "Premium1", LayoverTime = TimeSpan.FromMinutes(20), Note = "ahdjbsad"  },
                new IntermediateAirport { ID = "3", Name = "Premium2", LayoverTime = TimeSpan.FromMinutes(40), Note = "ahdjbsad"  },
                new IntermediateAirport { ID = "4", Name = "VIP1", LayoverTime = TimeSpan.FromMinutes(50), Note = "ahdjbsad"  },
                new IntermediateAirport { ID = "5", Name = "VIP2", LayoverTime = TimeSpan.FromMinutes(25), Note = "ahdjbsad"  },
                new IntermediateAirport { ID = "6", Name = "Elon Musk", LayoverTime = TimeSpan.FromMinutes(1) }
            };
            collectionViewIA = CollectionViewSource.GetDefaultView(IAList);
            dataGrid2.ItemsSource = collectionViewIA;
        }
        private void ConfirmSchedule_Click(object sender, RoutedEventArgs e)
        {

            // Xử lí ......


            // Nếu thành công/hợp lệ - reset dữ liệu trên màn hình để nhập tiếp...
            bool f = true;
            if (f) {
                SourceAirport.SelectedIndex = -1;
                DestinationAirport.SelectedIndex = -1;
                //...
            }
            else
            {
                // Xuat man hinh bao loi
            }
            // Tu bi không tình yêu
        }


        /*---------------------------------------------BEGIN Data Grid 1 aka TicketClass------------------------------------------------*/

        private void EndEditTicket()
        {
            dataGrid1.CancelEdit(DataGridEditingUnit.Row);
            dataGrid1.IsReadOnly = true;
            foreach (var item in dataGrid1.Items)
            {
                DataGridRow otherDataGridRow = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromItem(item);
                if (otherDataGridRow != null)
                {
                    otherDataGridRow.IsEnabled = true;
                }
                if (item is TicketClass ticket)
                {
                    ticket.ButtonContent = "Edit";
                }
            }
        }
        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            EndEditTicket();
            var newTicket = new TicketClass { ID = "000000", Name = "Default", Quantity = 0 };
            ticketList.Add(newTicket);
            collectionViewTicketClass.MoveCurrentTo(newTicket);
            
        }
        private void ResetTicket_Click(object sender, RoutedEventArgs e)
        {
            EndEditTicket();
            ticketList.Clear();
        }
        private void DeleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            TicketClass selectedTicket = (TicketClass)dataGrid1.SelectedItem;
            if (selectedTicket != null)
            {   
                if(dataGrid1.IsReadOnly == false)
                {
                    EndEditTicket();
                }
                ((ObservableCollection<TicketClass>)collectionViewTicketClass.SourceCollection).Remove(selectedTicket);
                dataGrid1.ItemsSource = collectionViewTicketClass;
            }
        }

        private void EditButton_Click_1(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            TicketClass ticket = editButton.CommandParameter as TicketClass;
            if (ticket.ButtonContent == "Edit")
            {
                ticket.ButtonContent = "End";
                DependencyObject dependencyObject = editButton;
                while ((dependencyObject = VisualTreeHelper.GetParent(dependencyObject)) != null && !(dependencyObject is DataGridRow)){}

                DataGridRow row = dependencyObject as DataGridRow;

                if (row != null)
                {
                    dataGrid1.IsReadOnly = false;
                    dataGrid1.SelectedItem = row.Item;
                    dataGrid1.CurrentItem = row.Item;
                    dataGrid1.BeginEdit();

                    // Vô hiệu hóa tất cả các dòng khác ngoại trừ dòng đang được edit
                    foreach (var otherRow in dataGrid1.Items)
                    {
                        if (otherRow != row.Item)
                        {
                            DataGridRow otherDataGridRow = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromItem(otherRow);
                            if (otherDataGridRow != null)
                            {
                                otherDataGridRow.IsEnabled = false;
                            }
                        }
                    }

                    // Đợi sự kiện nhấn Enter
                    dataGrid1.PreviewKeyDown += DataGrid1_PreviewKeyDown;
                }
            }
            else
            {
                EndEditTicket();
            }
        }

        private void DataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && dataGrid1.CommitEdit(DataGridEditingUnit.Row, true))
            {
                EndEditTicket();
                // Gỡ việc đợi nhấn phím Enter
                dataGrid1.PreviewKeyDown -= DataGrid1_PreviewKeyDown;
            }
        }

        /*---------------------------------------------END Data Grid 1 aka TicketClass------------------------------------------------*/

        /*---------------------------------------------BEGIN Data Grid 2 aka IA-------------------------------------------------------*/
        private void EndEditIA()
        {
            dataGrid2.CancelEdit(DataGridEditingUnit.Row);
            dataGrid2.IsReadOnly = true;
            foreach (var item in dataGrid2.Items)
            {
                DataGridRow otherDataGridRow = (DataGridRow)dataGrid2.ItemContainerGenerator.ContainerFromItem(item);
                if (otherDataGridRow != null)
                {
                    otherDataGridRow.IsEnabled = true;
                }
                if (item is IntermediateAirport IA)
                {
                    IA.ButtonContent = "Edit";
                }
            }
        }

        private void AddIA_Click(object sender, RoutedEventArgs e)
        {
            EndEditIA();
            var newIA = new IntermediateAirport { ID = "Default", Name = "Default", LayoverTime = TimeSpan.FromSeconds(0), Note = "...XYZ" };
            IAList.Add(newIA);
            collectionViewTicketClass.MoveCurrentTo(newIA);
        }

        private void ResetIA_Click(object sender, RoutedEventArgs e)
        {
            EndEditIA();
            IAList.Clear();
        }

        private void DeleteButton_Click_2(object sender, RoutedEventArgs e)
        {
            IntermediateAirport selectedIA = (IntermediateAirport)dataGrid2.SelectedItem;
            if (selectedIA != null)
            {
                if (dataGrid2.IsReadOnly == false)
                {
                    EndEditIA();
                }
                ((ObservableCollection<IntermediateAirport>)collectionViewTicketClass.SourceCollection).Remove(selectedIA);
                dataGrid2.ItemsSource = collectionViewIA;
            }
        }

        private void EditButton_Click_2(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            IntermediateAirport IA = editButton.CommandParameter as IntermediateAirport;
            if (IA.ButtonContent == "Edit")
            {
                IA.ButtonContent = "End";
                DependencyObject dependencyObject = editButton;
                while ((dependencyObject = VisualTreeHelper.GetParent(dependencyObject)) != null && !(dependencyObject is DataGridRow)) { }

                DataGridRow row = dependencyObject as DataGridRow;

                if (row != null)
                {
                    dataGrid2.IsReadOnly = false;
                    dataGrid2.SelectedItem = row.Item;
                    dataGrid2.CurrentItem = row.Item;
                    dataGrid2.BeginEdit();

                    // Vô hiệu hóa tất cả các dòng khác ngoại trừ dòng đang được edit
                    foreach (var otherRow in dataGrid2.Items)
                    {
                        if (otherRow != row.Item)
                        {
                            DataGridRow otherDataGridRow = (DataGridRow)dataGrid2.ItemContainerGenerator.ContainerFromItem(otherRow);
                            if (otherDataGridRow != null)
                            {
                                otherDataGridRow.IsEnabled = false;
                            }
                        }
                    }

                    // Đợi sự kiện nhấn Enter
                    dataGrid2.PreviewKeyDown += DataGrid2_PreviewKeyDown;
                }
            }
            else
            {
                EndEditIA();
            }
        }

        private void DataGrid2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && dataGrid2.CommitEdit(DataGridEditingUnit.Row, true))
            {
                EndEditIA();
                // Gỡ việc đợi nhấn phím Enter
                dataGrid2.PreviewKeyDown -= DataGrid2_PreviewKeyDown;
            }
        }

        /*---------------------------------------------END Data Grid 2 aka IA---------------------------------------------------------*/

    }



    public class TicketClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        private string _name;
        private int _quantity;
        private string _buttonContent = "Edit";

        public string ID { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public string ButtonContent
        {
            get => _buttonContent;
            set { _buttonContent = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class IntermediateAirport : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        private string _name;
        private TimeSpan _layoverTime;
        private string _note;
        private string _buttonContent = "Edit";

        public string ID { get => _id; set { if (_id != value) { _id = value; OnPropertyChanged(); } } }

        public string Name { get => _name; set { if (_name != value) { _name = value; OnPropertyChanged(); } } }

        public TimeSpan LayoverTime { get => _layoverTime; set { if ( _layoverTime != value) { _layoverTime = value; OnPropertyChanged(); } } }

        public string Note { get => _note; set { if (_note != value) { _note = value; OnPropertyChanged(); } } }

        public string ButtonContent
        {
            get => _buttonContent;
            set { _buttonContent = value; OnPropertyChanged(); }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
