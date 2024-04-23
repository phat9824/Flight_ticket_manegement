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
using ControlzEx.Standard;
using BLL;
using System.Data.SqlClient;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    /// 

    /*
     Xong:
        Convert dữ liệu sang DTO
        Sync ID-Name
        Giới hạn số ticket class và IA theo parameter

     Cần:
        Tiếp tục giới hạn miền giá trị thuộc tính theo parameter
        Màn hình báo lỗi
     */
    public partial class FlightScheduleWindow : UserControl
    {
        Flight_BLL fl_bll = new Flight_BLL();

        private ObservableCollection<TicketClass> ticketList;
        private TicketClass defaultTicketClass = new TicketClass { ID = "Default", Name = "Default", Quantity = -1 };
        private ObservableCollection<IntermediateAirport> IAList; // Intermidate Airport List
        private IntermediateAirport defaultIA = new IntermediateAirport { ID = "Default", Name = "Default", LayoverTime = TimeSpan.FromMinutes(0), Note = "..." };
        private ICollectionView collectionViewTicketClass;
        private ICollectionView collectionViewIA;

        public ParameterDTO parameterDTO;
        public List<TicketClassDTO> ticketClasses { get; set; }
        public List<AirportDTO> airports {  get; set; }
        public FlightScheduleWindow()
        {
            InitializeComponent();
            
            //---------------------------------------------------------------------------------------------------------------------------------------------
            //parameterDTO = BAL.GetParameter();

            parameterDTO = new ParameterDTO();
            parameterDTO.IntermediateAirportCount = 2;
            parameterDTO.TicketClassCount = 2;

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // airports = BAL.GetAirports();
            // ticketclasses =BAL.GetTicketClass();

            airports = fl_bll.L_airport();
            ticketClasses = fl_bll.L_TicketClass();

            //----------------------------------------------------------------------------------------------------------------------------------
            ticketList = new ObservableCollection<TicketClass>
            {
                defaultTicketClass,
            };
            collectionViewTicketClass = CollectionViewSource.GetDefaultView(ticketList);
            dataGrid1.ItemsSource = collectionViewTicketClass;

            IAList = new ObservableCollection<IntermediateAirport>
            {
                defaultIA,
                defaultIA,
            };
            collectionViewIA = CollectionViewSource.GetDefaultView(IAList);
            dataGrid2.ItemsSource = collectionViewIA;

            DestinationAirport.ItemsSource = airports;
            DestinationAirportID.ItemsSource = airports;
            SourceAirport.ItemsSource = airports;
            SourceAirportID.ItemsSource = airports;
            DataContext = this;
        }
        private void ConfirmSchedule_Click(object sender, RoutedEventArgs e)
        {
            if(FlightDay.SelectedDate == null && !FlightTime.SelectedTime.HasValue)
            {
                MessageBox.Show("Vui lòng nhập ngày và thời gian khởi hành!", "Error");
                return;
            }
            else if(FlightDay.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng nhập ngày khời hành!", "Error");
                return;
            }
            else if (!FlightTime.SelectedTime.HasValue)
            {
                MessageBox.Show("Vui lòng nhập thời gian khời hành!", "Error");
                return;
            }
            if (FlightDay.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Vui lòng chọn ngày khởi hành bắt đầu từ " + DateTime.Today.ToString("MM/dd/yyyy"), "Error");
                return;
            }
            else if(FlightDay.SelectedDate == DateTime.Today && FlightTime.SelectedTime < DateTime.Now)
            {
                MessageBox.Show("Vui lòng chọn ngày khởi hành bắt đầu từ " + DateTime.Now.ToString("MM/dd/yyyy h:m:s tt"), "Error");
                return; 
            }
            ScheduleData data = GetScheduleData();
            FlightDTO flightDTO = data.InitializeFlightDTO();

            fl_bll.Add_Flights(flightDTO);

            List<TicketClassFlightDTO> listTicketClassFlightDTO = data.InitializeListTicketClassFlightDTO();
            List<IntermediateAirportDTO> listIntermediateAirportDTO = data.InitializeListIntermediateAirportDTO();

            // Xử lí ......
            // var processStateInfor = BAL.ProcessObject.processMethod(flightDTO, listTicketClassFlightDTO, listIntermediateAirportDTO);

            // Debug datatype bằng cách huyền thoại, sẽ được xóa sau
            MessageBox.Show(data.ToString(), "CheckData");


            // Nếu thành công/hợp lệ - reset dữ liệu trên màn hình để nhập tiếp
            string processStateInfor = string.Empty;
            if (String.IsNullOrWhiteSpace(processStateInfor)) {
                ResetDataWindow();
                var newTicket = defaultTicketClass;
                ticketList.Add(newTicket);
                collectionViewTicketClass.MoveCurrentTo(newTicket);
            }
            else
            {
                string errorMessage = "An error occurred. Please check the data and try again.";
                string state = "Error";
                MessageBox.Show(errorMessage, state);
            }
        }

        private void ResetDataWindow()
        {
            SourceAirport.SelectedIndex = -1;
            SourceAirport.Text = string.Empty;
            DestinationAirport.SelectedIndex = -1;
            DestinationAirport.Text = string.Empty;
            SourceAirportID.SelectedIndex = -1;
            SourceAirportID.Text = string.Empty;
            DestinationAirportID.SelectedIndex = -1;
            DestinationAirportID.Text = string.Empty;
            FlightID.Text = string.Empty;
            TicketPrice.Text = string.Empty;
            FlightDay.SelectedDate = null;
            FlightTime.SelectedTime = null;
            ticketList.Clear();
            IAList.Clear();
            collectionViewTicketClass.Filter = null;
            collectionViewIA.Filter = null;
        }

        private ScheduleData GetScheduleData()
        {
            ScheduleData data = new ScheduleData();
            data.sourceAirportID = SourceAirportID.Text.Trim();
            data.destinationAirportID = DestinationAirportID.Text.Trim();
            data.flightID = fl_bll.AutoID();
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

            var newTicket = defaultTicketClass;
            ticketList.Add(newTicket);
            collectionViewTicketClass.MoveCurrentTo(newTicket);
            
        }
        private void ResetTicket_Click(object sender, RoutedEventArgs e)
        {
            ticketList.Clear();
            var newTicket = defaultTicketClass;
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

        private void AddIA_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.Items.Count >= parameterDTO.TicketClassCount)
            {
                MessageBox.Show($"Cannot add intermidate airport. The maximum intermidate airport is {parameterDTO.IntermediateAirportCount}");
                return;
            }

            var newIA = defaultIA;
            IAList.Add(newIA);
            collectionViewTicketClass.MoveCurrentTo(newIA);
        }

        private void ResetIA_Click(object sender, RoutedEventArgs e)
        {
            IAList.Clear();
        }

        private void DeleteButton_Click_2(object sender, RoutedEventArgs e)
        {
            IntermediateAirport selectedIA = (IntermediateAirport)dataGrid2.SelectedItem;
            if (selectedIA != null)
            {
                ((ObservableCollection<IntermediateAirport>)collectionViewIA.SourceCollection).Remove(selectedIA);
                dataGrid2.ItemsSource = collectionViewIA;
            }
        }

        private void ComboBox_AirportID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBoxIAID = sender as ComboBox;
            if (comboBoxIAID.SelectedItem is AirportDTO selectedIA)
            {
                var dataGridRow = DataGridRow.GetRowContainingElement(comboBoxIAID);
                var comboBoxIAName = FindChild<ComboBox>(dataGridRow, "ComboBoxIAName");

                if (comboBoxIAName != null)
                {
                    comboBoxIAName.SelectedValue = selectedIA.AirportName;
                }
            }
        }

        private void ComboBox_AirportName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBoxIAName = sender as ComboBox;
            if (comboBoxIAName.SelectedItem is AirportDTO selectedIA)
            {
                var dataGridRow = DataGridRow.GetRowContainingElement(comboBoxIAName);
                var comboBoxIAID = FindChild<ComboBox>(dataGridRow, "ComboBoxIAID");

                if (comboBoxIAID != null)
                {
                    comboBoxIAID.SelectedValue = selectedIA.AirportID;
                }
            }
        }

        /*---------------------------------------------END Data Grid 2 aka IA---------------------------------------------------------*/

    }
}
