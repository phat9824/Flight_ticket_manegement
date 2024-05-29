using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GUI.ViewModel;
using MaterialDesignThemes.Wpf;
using System.Collections;
using System.Windows.Media;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
        private List<SortPropertyPair> SortProperties = new List<SortPropertyPair>
        {
            new SortPropertyPair { Key = "Seq", Value = "STT"},
            new SortPropertyPair { Key= "Departure airport", Value = "SanBayDi"},
            new SortPropertyPair { Key = "Destination airport", Value = "SanBayDen"},
            new SortPropertyPair { Key = "Departure time", Value = "KhoiHanh"},
            new SortPropertyPair { Key = "Duration", Value = "ThoiGian"},
            new SortPropertyPair { Key = "Empty seat", Value = "SoGheDat"},
            new SortPropertyPair { Key = "Booked seat", Value = "SoGheTrong"}
        };
        private Dictionary<string, string> airportDictionary = new Dictionary<string, string>();

        public List<AirportDTO> airports { get; set; }
        public Window2()
        {
            InitializeComponent();
            var converter = new BrushConverter();

            // Create DataGrid Items
            flights.Add(new Flight { STT = "1", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "2", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "3", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "4", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            flights.Add(new Flight { STT = "5", SanBayDi = "", SanBayDen = "", KhoiHanh = "", ThoiGian = "", SoGheDat = "", SoGheTrong = "" });
            FlightsDataGrid.ItemsSource = flights;

            Airport_BLL airport_bll = new Airport_BLL();

            // Dùng cho item source
            airports = airport_bll.L_airport();
            // Dùng cho xử lý nếu cần
            airportDictionary = airports.ToDictionary(airport => airport.AirportID, airport => airport.AirportName);

            SourceAirport.ItemsSource = airports;
            DestinationAirport.ItemsSource = airports;

            SortProperty.ItemsSource = SortProperties;
            SortProperty.DisplayMemberPath = "Key";
            SortProperty.SelectedValuePath = "Value";
        }

        private bool IsEmpty(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            if (value is ICollection collection)
            {
                return collection.Count == 0;
            }

            if (value is Array array)
            {
                return array.Length == 0;
            }

            // Add more type checks as needed

            return false;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string a = SourceAirport.SelectedValue as string;
            string b = DestinationAirport.SelectedValue as string;

            // Check if SourceAirport or DestinationAirport are empty
            if (IsEmpty(a) || IsEmpty(b))
            {
                MessageBox.Show("Source or Destination airport cannot be empty.");
                return;
            }

            // 2 Giá trị dưới là min và max cho phép của SQL
            DateTime startDate = StartDay.SelectedDate.HasValue ? StartDay.SelectedDate.Value.Date : new DateTime(1753, 1, 1, 0, 0, 0);
            DateTime endDate = EndDay.SelectedDate.HasValue ? EndDay.SelectedDate.Value.Date : new DateTime(9999, 12, 31, 23, 59, 59);

            List<FlightInforDTO> flightInformationSearches = new List<FlightInforDTO>();
            flightInformationSearches = new BLL.SearchProcessor().GetFlightInfoDTO(a, b, startDate, endDate);
            flights = Flight.ConvertListToObservableCollection(flightInformationSearches, airportDictionary);

            // Nếu có yêu cầu sort
            if (SortProperty.SelectedIndex != -1)
            {
                string sortOrder = "ASC";
                flights = SearchProcessor.SortItems<Flight>(flights, SortProperty.SelectedValue.ToString(), sortOrder);
            }

            FlightsDataGrid.ItemsSource = flights;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit f = new Edit();
            f.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sortOrder = "ASC";
            flights = SearchProcessor.SortItems<Flight>(flights, SortProperty.SelectedValue.ToString(), sortOrder);
            FlightsDataGrid.ItemsSource = flights;
        }
    }
}
