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

        private int maxNumTicket = 0;
        private int numTicket = 0;
        private Int64 ticketPrice = 0;

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
            ViewCustomerData = new ObservableCollection<CustomerDTO>();
            numTicket = ViewCustomerData.Count;

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
                numTicket = ViewCustomerData.Count;
                TicketQuantity.Text = numTicket.ToString();
                TotalPrice.Text = (numTicket * ticketPrice).ToString();
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
                    //TicketClass.Text = ticketClassDictionary[TicketClass_popup.SelectedValue.ToString()];
                    if (TicketClass_popup.SelectedValue != null)
                    {
                        TicketClass.Text = ticketClassDictionary[TicketClass_popup.SelectedValue.ToString()];
                    }
                    else
                    {
                        TicketClass.Text = "All";
                    }
                    TicketPrice.Text = selectedFlightInfo.Flight.Price.ToString();
                    maxNumTicket = selectedFlightInfo.emptySeats;
                    TicketQuantity.Text = maxNumTicket.ToString();
                    TotalPrice.Text = (numTicket * selectedFlightInfo.Flight.Price).ToString();

                    var cus = new ObservableCollection<CustomerDTO>();
                    for (int i = 0; i < maxNumTicket; i++)
                    {
                        cus.Add(new CustomerDTO { ID = "", CustomerName = "", Phone = "", Email = "", Birth = new DateTime(2000, 1, 1) });
                    }
                    ViewCustomerData = cus;
                    customerView = CollectionViewSource.GetDefaultView(ViewCustomerData);
                    MyListView.ItemsSource = customerView;
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
            string SourceAirport = (SourceAirport_popup.SelectedValue == null ? "" : SourceAirport_popup.SelectedValue.ToString());
            string DestinationAirport = (DestinationAirport_popup.SelectedValue == null ? "" : DestinationAirport_popup.SelectedValue.ToString());
            string TicketClass = (TicketClass_popup.SelectedValue == null ? "" : TicketClass_popup.SelectedValue.ToString());
            DateTime DepartureDay1 = (DepartureDay_popup.SelectedDate.HasValue ? DepartureDay_popup.SelectedDate.Value.Date : new DateTime(2024,1,1));
            DateTime DepartureDay2 = (DepartureDay_popup.SelectedDate.HasValue ? DepartureDay_popup.SelectedDate.Value.Date.AddDays(1).AddTicks(-1) : new DateTime(3000,1,1));

            List<FlightInforDTO> flights = new BLL.SearchProcessor().GetFlightInfoDTO(SourceAirport, DestinationAirport, DepartureDay1, DepartureDay2, TicketClass,
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
            SourceAirport_popup.SelectedIndex = -1;
            DestinationAirport_popup.SelectedIndex = -1;
            DepartureDay_popup.SelectedDate = null;
            TicketClass_popup.SelectedIndex = -1;
            SearchFlight_Popup.IsOpen = false;
        }
    }
}

