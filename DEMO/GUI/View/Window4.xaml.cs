using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
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
using DTO;
using GUI.ViewModel;
using System.Collections;
using System.Web.UI.WebControls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    /// 

    /*
     Đang:
        Convert sang dữ liệu sang DTO (gần xong)
        Thêm AirportName vào SourceAirport và DestinationAirport
     
     Cần:
        Giới hạn miền giá trị thuộc tính theo parameter
        Màn hình báo lỗi
        Kiểm tra tính hợp lệ dữ liệu không liên quan database
     */
    public partial class Window4 : UserControl
    {
        private ObservableCollection<TicketClass> ticketList;
        private ObservableCollection<IntermediateAirport> IAList; // Intermidate Airport List
        private ICollectionView collectionViewTicketClass;
        private ICollectionView collectionViewIA;
        private ParameterDTO parameterDTO;
        public List<TicketClassDTO> ticketClasses { get; set; }

        public Window4()
        {
            InitializeComponent();
            
            //---------------------------------------------------------------------------------------------------------------------------------------------
            //parameterDTO = getParameterBAL();

            parameterDTO = new ParameterDTO();
            parameterDTO.IntermediateAirportCount = 2;
            parameterDTO.TicketClassCount = 2;

            //---------------------------------------------------------------------------------------------------------------------------------------------
            //List<AirportDTO> airports = BAL.GetAirports();
            //List<TicketClassDTO> ticketclasses =BAl.GetTicketClass();

            List<AirportDTO> airports = new List<AirportDTO>
            {
                new AirportDTO() {AirportID = "000", AirportName = "Test"},
                new AirportDTO() {AirportID = "001", AirportName = "Tân Sơn Nhất"},
                new AirportDTO() {AirportID = "002", AirportName = "Nội Bài"},
            };
            DestinationAirport.ItemsSource = airports;
            DestinationAirportID.ItemsSource = airports;
            SourceAirport.ItemsSource = airports;
            SourceAirportID.ItemsSource = airports;

            ticketClasses = new List<TicketClassDTO>
            {
            new TicketClassDTO { TicketClassID = "1", TicketClassName = "Economy" },
            new TicketClassDTO { TicketClassID = "2", TicketClassName = "Business" },
            };
            //InitializeComboBoxItems();

            //----------------------------------------------------------------------------------------------------------------------------------
            ticketList = new ObservableCollection<TicketClass>
            {
                new TicketClass { ID = "Default", Name = "Default", Quantity = -1 },
            };
            collectionViewTicketClass = CollectionViewSource.GetDefaultView(ticketList);
            dataGrid1.ItemsSource = collectionViewTicketClass;

            IAList = new ObservableCollection<IntermediateAirport>
            {
                new IntermediateAirport { ID = "1", Name = "Standard", LayoverTime = TimeSpan.FromMinutes(15), Note = "ahdjbsad" },
                new IntermediateAirport { ID = "2", Name = "Premium1", LayoverTime = TimeSpan.FromMinutes(20), Note = "ahdjbsad"  },
            };
            collectionViewIA = CollectionViewSource.GetDefaultView(IAList);
            dataGrid2.ItemsSource = collectionViewIA;

            DataContext = this;
        }
        private void ConfirmSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleData data = GetScheduleData();
            FlightDTO flightDTO = data.InitializeFlightDTO();
            List<TicketClassFlightDTO> listTicketClassFlightDTO = data.InitializeListTicketClassFlightDTO();
            List<IntermediateAirportDTO> listIntermediateAirportDTO = data.InitializeListIntermediateAirportDTO();

            // Xử lí ......
            // var processStateInfor = BAL.ProcessObject.processMethod(flightDTO,airportDTO,listTicketClassDTO,listTicketClassFlightDTO,listIntermediateAirportDTO);
            
            // Debug datatype bằng cách in ra màn hình vì không biết dùng công cụ debug =)), sẽ được xóa sau
            MessageBox.Show(data.ToString(), "CheckData");


            // Nếu thành công/hợp lệ - reset dữ liệu trên màn hình để nhập tiếp
            bool f = true;
            if (f) {
                SourceAirport.SelectedIndex = -1;
                SourceAirport.Text = string.Empty;
                DestinationAirport.SelectedIndex = -1;
                DestinationAirport.Text = string.Empty;
                FlightID.Text = string.Empty;
                TicketPrice.Text = string.Empty;
                FlightDay.SelectedDate = null;
                FlightTime.SelectedTime = null;
                ticketList.Clear();
                IAList.Clear();
                collectionViewTicketClass.Filter = null;
                collectionViewIA.Filter = null;
            }
            else
            {
                MessageBox.Show("An error occurred. Please check the data and try again.", "Error");
            }
        }

        private ScheduleData GetScheduleData()
        {
            ScheduleData data = new ScheduleData();
            data.sourceAirportID = SourceAirportID.Text.Trim();
            data.destinationAirportID = DestinationAirportID.Text.Trim();
            data.flightID = FlightID.Text.Trim();
            data.price = decimal.TryParse(TicketPrice.Text.Trim(), out decimal price) ? price : -1;
            data.flightDay = FlightDay.SelectedDate ?? DateTime.MinValue;
            data.flightTime = FlightTime.SelectedTime.HasValue ? FlightTime.SelectedTime.Value.TimeOfDay : TimeSpan.Zero;
            data.IAList = IAList;
            data.ticketList = ticketList;
            return data;
        }

        /*---------------------------------------------BEGIN R1------------------------------------------------*/

        private void DestinationAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DestinationAirport.SelectedItem is AirportDTO selectedAirport)
            {
                DestinationAirportID.SelectedValue = selectedAirport.AirportID;
            }
        }

        private void DestinationAirportID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DestinationAirportID.SelectedItem is AirportDTO selectedAirport)
            {
                DestinationAirport.SelectedValue = selectedAirport.AirportID;
            }
        }

        private void SourceAirport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SourceAirport.SelectedItem is AirportDTO selectedAirport)
            {
                SourceAirportID.SelectedValue = selectedAirport.AirportID;
            }
        }

        private void SourceAirportID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SourceAirportID.SelectedItem is AirportDTO selectedAirport)
            {
                SourceAirport.SelectedValue = selectedAirport.AirportID;
            }
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem == null && !string.IsNullOrEmpty(comboBox.Text))
            {
                comboBox.Text = "";
            }
        }

        /*---------------------------------------------END R1--------------------------------------------------*/

        /*---------------------------------------------BEGIN Data Grid 1 aka TicketClass------------------------------------------------*/

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.Items.Count >= parameterDTO.TicketClassCount)
            {
                MessageBox.Show($"Cannot add ticket class. The maximum ticket class is {parameterDTO.TicketClassCount}");
                return;
            }

            var newTicket = new TicketClass { ID = "Default", Name = "Default", Quantity = -1 };
            ticketList.Add(newTicket);
            collectionViewTicketClass.MoveCurrentTo(newTicket);
            
        }
        private void ResetTicket_Click(object sender, RoutedEventArgs e)
        {
            ticketList.Clear();
            var newTicket = new TicketClass { ID = "Default", Name = "Default", Quantity = -1 };
            ticketList.Add(newTicket);
            collectionViewTicketClass.MoveCurrentTo(newTicket);
        }
        private void DeleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.Items.Count <= 1)
            {
                MessageBox.Show($"Cannot delete ticket class. The minimum ticket class is 1");
                return;
            }
            TicketClass selectedTicket = (TicketClass)dataGrid1.SelectedItem;
            if (selectedTicket != null)
            {   
                ((ObservableCollection<TicketClass>)collectionViewTicketClass.SourceCollection).Remove(selectedTicket);
                dataGrid1.ItemsSource = collectionViewTicketClass;
            }
        }

        private void ComboBox_TicketClassID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBoxID = sender as ComboBox;
            if (comboBoxID.SelectedItem is TicketClassDTO selectedTicketClass)
            {
                var dataGridRow = DataGridRow.GetRowContainingElement(comboBoxID);
                var comboBoxName = FindChild<ComboBox>(dataGridRow, "ComboBoxName");

                if (comboBoxName != null)
                {
                    comboBoxName.SelectedValue = selectedTicketClass.TicketClassName;
                }
            }
        }

        private void ComboBox_TicketClassName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBoxName = sender as ComboBox;
            if (comboBoxName.SelectedItem is TicketClassDTO selectedTicketClass)
            {
                var dataGridRow = DataGridRow.GetRowContainingElement(comboBoxName);
                var comboBoxID = FindChild<ComboBox>(dataGridRow, "ComboBoxID");

                if (comboBoxID != null)
                {
                    comboBoxID.SelectedValue = selectedTicketClass.TicketClassID;
                }
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
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

            if (dataGrid2.Items.Count >= parameterDTO.TicketClassCount)
            {
                MessageBox.Show($"Cannot add intermidate airport. The maximum intermidate airport is {parameterDTO.IntermediateAirportCount}");
                return;
            }

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
                ((ObservableCollection<IntermediateAirport>)collectionViewIA.SourceCollection).Remove(selectedIA);
                dataGrid2.ItemsSource = collectionViewIA;
            }
        }

        private void EditButton_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button editButton = sender as System.Windows.Controls.Button;
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
}
