using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using GUI.Model;

namespace GUI.ViewModel
{
    class Window1VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Window1
        {
            get { return _pageModel.Window1; }
            set { _pageModel.Window1 = value; OnPropertyChanged(); }
        }

        public Window1VM()
        {
            _pageModel = new PageModel();
        }
    }
}

namespace GUI.ViewModel.StaffWin1
{
    public class FlightData
    {
        public string flightID { get; set; }
        public string remainingTickets { get; set; }
        public string ticketPrice { get; set; }
        public string departureTime { get; set; }

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
    }
}
