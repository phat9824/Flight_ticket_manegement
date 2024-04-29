using BLL;
using DTO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUI.ViewModel;
using System.Windows.Media.Media3D;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : UserControl
    {
        ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
        private List<string> suggestions = new List<string> { "Gợi ý 1", "Gợi ý 2", "Gợi ý 3" };
        
        public List<AirportDTO> airports { get; set; }
        private Dictionary<string, string> airportDictionary = new Dictionary<string, string>();
        public Window2()
        {
            InitializeComponent();
            var converter = new BrushConverter();

            //Create DataGrid Items

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
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    textInput.Visibility = Visibility.Visible;
        //    textInput.Focus();
        //}

        //private void textInput_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    string input = textInput.Text.ToLower();
        //    suggestionListBox.Items.Clear();
        //    foreach (string suggestion in suggestions)
        //    {
        //        if (suggestion.ToLower().Contains(input))
        //        {
        //            suggestionListBox.Items.Add(suggestion);
        //        }
        //    }
        //    suggestionListBox.Visibility = Visibility.Visible;
        //}


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string a = SourceAirport.SelectedValue as string;
            string b = DestinationAirport.SelectedValue as string;

            // 2 Giá trị dưới là min và max cho phép của SQL
            DateTime startDate = StartDay.SelectedDate.HasValue ? StartDay.SelectedDate.Value.Date : new DateTime(1753, 1, 1, 0, 0, 0);
            DateTime endDate = EndDay.SelectedDate.HasValue ? EndDay.SelectedDate.Value.Date : new DateTime(9999, 12, 31, 23, 59, 59);

            SearchProcessor search = new SearchProcessor();
            List<FlightInformationSearchDTO> flightInformationSearches = new List<FlightInformationSearchDTO>();
            flightInformationSearches = search.GetInformationSearch(a, b, startDate, endDate);
            FlightsDataGrid.ItemsSource = Flight.ConvertListToObservableCollection(flightInformationSearches, airportDictionary);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit f = new Edit();
            f.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
