using BLL;
using ControlzEx.Standard;
using DTO;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : UserControl
    {
        private ObservableCollection<CustomerDTO> ViewCustomerData { get; set; }
        public ObservableCollection<FlightInforDTO> Flights { get; set; }
        private ICollectionView customerView;

        public List<AirportDTO> airports { get; set; }
        private Dictionary<string, string> airportDictionary = new Dictionary<string, string>();
        public List<TicketClassDTO> ticketClasses { get; set; }
        private Dictionary<string, string> ticketClassDictionary = new Dictionary<string, string>();
        public ICommand DeleteCommand { get; private set; }
        public Window6()
        {
            InitializeComponent();
            //this.Loaded += Popup_Loaded;
            //Application.Current.Deactivated += Popup_Deactivated;

            // Test data
            ViewCustomerData = new ObservableCollection<CustomerDTO>
            {
            new CustomerDTO { ID = "ID1", CustomerName = "Customer 1", Phone = "5551234501", Email = "customer1@example.com", Birth = new DateTime(1991, 1, 1) },
            new CustomerDTO { ID = "ID2", CustomerName = "Customer 2", Phone = "5551234502", Email = "customer2@example.com", Birth = new DateTime(1992, 1, 1) },
            new CustomerDTO { ID = "ID3", CustomerName = "Customer 3", Phone = "5551234503", Email = "customer3@example.com", Birth = new DateTime(1993, 1, 1) },
            new CustomerDTO { ID = "ID4", CustomerName = "Customer 4", Phone = "5551234504", Email = "customer4@example.com", Birth = new DateTime(1994, 1, 1) },
            new CustomerDTO { ID = "ID5", CustomerName = "Customer 5", Phone = "5551234505", Email = "customer5@example.com", Birth = new DateTime(1995, 1, 1) },
            new CustomerDTO { ID = "ID6", CustomerName = "Customer 6", Phone = "5551234506", Email = "customer6@example.com", Birth = new DateTime(1996, 1, 1) },
            new CustomerDTO { ID = "ID7", CustomerName = "Customer 7", Phone = "5551234507", Email = "customer7@example.com", Birth = new DateTime(1997, 1, 1) },
            new CustomerDTO { ID = "ID8", CustomerName = "Customer 8", Phone = "5551234508", Email = "customer8@example.com", Birth = new DateTime(1998, 1, 1) },
            new CustomerDTO { ID = "ID9", CustomerName = "Customer 9", Phone = "5551234509", Email = "customer9@example.com", Birth = new DateTime(1999, 1, 1) },
            new CustomerDTO { ID = "ID10", CustomerName = "Customer 10", Phone = "5551234510", Email = "customer10@example.com", Birth = new DateTime(2000, 1, 1) }
            };
            customerView = CollectionViewSource.GetDefaultView(ViewCustomerData);
            MyListView.ItemsSource = customerView;

            Airport_BLL airport_bll = new Airport_BLL();
            Ticket_Class_BLL ticket_class_bll = new Ticket_Class_BLL();
            airports = airport_bll.L_airport();
            ticketClasses = ticket_class_bll.L_TicketClass();
            airportDictionary = airports.ToDictionary(airport => airport.AirportID, airport => airport.AirportName);
            ticketClassDictionary = ticketClasses.ToDictionary(ticketClass => ticketClass.TicketClassID, ticketClass => ticketClass.TicketClassName);
            SourceAirport_popup.ItemsSource = airports;
            DestinationAirport_popup.ItemsSource = airports;
            TicketClass_popup.ItemsSource = ticketClasses;

            // Test data
           /* var flights = new List<FlightInforDTO>
            {
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1001",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1002",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1003",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1004",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1005",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1006",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1007",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1008",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL1009",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL10010",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL10011",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL10012",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL10013",SourceAirportID = "S101",DestinationAirportID = "D201",FlightDay = DateTime.Today.AddDays(1),FlightTime = TimeSpan.FromHours(3),Price = 110.00m},bookedTickets = 52,emptySeats = 148},
            new FlightInforDTO{Flight = new FlightDTO{FlightID = "FL10014",SourceAirportID = "S102",DestinationAirportID = "D202",FlightDay = DateTime.Today.AddDays(2),FlightTime = TimeSpan.FromHours(4),Price = 120.00m},bookedTickets = 54,emptySeats = 146},
            };*/

            dataGridFlights.ItemsSource = Flights;

            DeleteCommand = new RelayCommand<object>(DeleteItem);
            DataContext = this;

        }
        private void DeleteItem(object parameter)
        {
            var itemToRemove = parameter as CustomerDTO;
            if (itemToRemove != null)
            {
                ViewCustomerData.Remove(itemToRemove);
            }
        }

        private void SelectButton_Click_1(object sender, RoutedEventArgs e)
        {
            Button selectButton = sender as System.Windows.Controls.Button;
            if (selectButton != null)
            {
                FlightInforDTO selectedFlightInfo = selectButton.DataContext as FlightInforDTO;
                if (selectedFlightInfo != null)
                {
                    SearchFlight_Popup.IsOpen = false;
                    FlightID.Text = selectedFlightInfo.Flight.FlightID;
                    DepartureAirport.Text = airportDictionary[selectedFlightInfo.Flight.SourceAirportID];
                    DestinationAirport.Text = airportDictionary[selectedFlightInfo.Flight.DestinationAirportID];
                    DepartureTime.Text = selectedFlightInfo.Flight.FlightDay.ToString("dd-MM-yyyy HH:mm");
                    Duration.Text = selectedFlightInfo.Flight.FlightTime.ToString(@"hh\:mm");
                    TicketClass.Text = ticketClassDictionary[TicketClass_popup.SelectedValue.ToString()];
                    TicketPrice.Text = selectedFlightInfo.Flight.Price.ToString();
                }
            }
        }

        private void SearchFlight_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBox.Show(SourceAirport_popup.SelectedValue.ToString() + " "
                            + DestinationAirport_popup.SelectedValue.ToString() + " "
                            + DepartureDay_popup.SelectedDate.Value.Date.ToString() + " "
                            + TicketClass_popup.SelectedValue.ToString() + " "
                            + DepartureDay_popup.SelectedDate.Value.Date.AddDays(1).AddTicks(-1).ToString() + " "
                            + "\n Chỉ dành cho debug");*/

            List<FlightInforDTO> flights = new BLL.SearchProcessor().GetFlightInfoDTO(SourceAirport_popup.SelectedValue.ToString(),
                                                               DestinationAirport_popup.SelectedValue.ToString(),
                                                               DepartureDay_popup.SelectedDate.Value.Date,
                                                               DepartureDay_popup.SelectedDate.Value.Date.AddDays(1).AddTicks(-1),
                                                               TicketClass_popup.SelectedValue.ToString(),
                                                               int.TryParse(NumTicket.Text, out int numTicket) ? numTicket : 0);
            dataGridFlights.ItemsSource = new ObservableCollection<FlightInforDTO>(flights);

            /*MessageBox.Show(flights.list.Count + flights.list[0].Flight.FlightID + flights.state + "\n Chỉ dùng cho debug", "Debug");*/
        }

        private void DepartureDay_popup_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void textMaChuyenBay_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtMaChuyenBay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textGiave_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtGiaVe_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FindFlight_Click(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = true;
        }

        private void Popup_Loaded(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = true;
        }

        private void Popup_Deactivated(object sender, EventArgs e)
        {
            SearchFlight_Popup.IsOpen = false;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            SearchFlight_Popup.IsOpen = false;
        }
    }
}

