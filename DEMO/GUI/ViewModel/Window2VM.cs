using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;
using DTO;
using BLL;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GUI.ViewModel
{
    class Window2VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window2
        {
            get { return _pageModel.Window2; }
            set { _pageModel.Window2 = value; OnPropertyChanged(); }
        }

        public Window2VM()
        {
            _pageModel = new PageModel();
        }
    }

    public class Flight
    {
        public string STT { get; set; }
        public string SanBayDi { get; set; }
        public string SanBayDen { get; set; }
        public string KhoiHanh { get; set; }
        public string ThoiGian { get; set; }
        public string SoGheTrong { get; set; }
        public string SoGheDat { get; set; }

        public static Flight ConvertToFlight(FlightInformationSearchDTO flightInfo, Dictionary<string, string> airportDictionary)
        {
            Flight flight = new Flight
            {
                STT = flightInfo.Flight.FlightID,
                SanBayDi = airportDictionary[flightInfo.Flight.SourceAirportID],
                SanBayDen = airportDictionary[flightInfo.Flight.DestinationAirportID],
                KhoiHanh = flightInfo.Flight.FlightDay.ToString("dd/MM/yyyy hh:mm"),
                ThoiGian = flightInfo.Flight.FlightTime.ToString(@"hh\:mm"),
                SoGheTrong = flightInfo.emptySeats.ToString(),
                SoGheDat = flightInfo.bookedTickets.ToString()
            };
            return flight;
        }

        public static ObservableCollection<Flight> ConvertListToObservableCollection(List<FlightInformationSearchDTO> flightInfos, Dictionary<string, string> airportDictionary)
        {
            ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
            foreach (var flightInfo in flightInfos)
            {
                flights.Add(ConvertToFlight(flightInfo, airportDictionary));
            }
            return flights;
        }
        public static ObservableCollection<Flight> SortFlights(ObservableCollection<Flight> flights, string propertyName, string sortOrder)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(Flight), propertyName);
            return SortCollection(flights, propertyInfo, sortOrder);
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo;
        }

        public static ObservableCollection<Flight> SortCollection(ObservableCollection<Flight> flights, PropertyInfo propertyInfo, string sortOrder)
        {
            IEnumerable<Flight> sortedFlights;
            if (sortOrder == "ASC")
            {
                sortedFlights = flights.OrderBy(f => propertyInfo.GetValue(f, null));
            }
            else
            {
                sortedFlights = flights.OrderByDescending(f => propertyInfo.GetValue(f, null));
            }
            return new ObservableCollection<Flight>(sortedFlights);
        }
    }
}
