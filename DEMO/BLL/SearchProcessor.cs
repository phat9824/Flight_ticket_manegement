using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.SqlServer.Server;
using System.Reflection;
using System.Collections.ObjectModel;

namespace BLL
{
    public class SearchProcessor
    {
        public SearchProcessor() { }
        public static List<FlightInformationSearchDTO> GetInformationSearch(string sourceAirportID, string destinationAirportID
                                                                       , DateTime startDate, DateTime endDate)
        {
            FlightAccess flightAccess = new FlightAccess();
            SellingTicketAcess sellingTicketAcess = new SellingTicketAcess();
            Ticket_classAccess ticket_ClassAccess = new Ticket_classAccess();

            
            List<FlightInformationSearchDTO> data = new List<FlightInformationSearchDTO>();
            List<FlightDTO> flights = new List<FlightDTO>();
            flights = flightAccess.getFlight(sourceAirportID, destinationAirportID, startDate, endDate);

            foreach (FlightDTO flight in flights)
            {
                FlightInformationSearchDTO flightInformationSearchDTO = new FlightInformationSearchDTO();
                flightInformationSearchDTO.Flight = flight;
                flightInformationSearchDTO.bookedTickets = sellingTicketAcess.getTicketSales_byFlightID(flight.FlightID);
                flightInformationSearchDTO.emptySeats = ticket_ClassAccess.getTotalSeat_byFlightID(flight.FlightID) - flightInformationSearchDTO.bookedTickets;
                data.Add(flightInformationSearchDTO);
            }
            return data;
        }


        public static ObservableCollection<T> SortItems<T>(ObservableCollection<T> items, string propertyName, string sortOrder)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(typeof(T), propertyName);
            return SortCollection(items, propertyInfo, sortOrder);
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo;
        }

        public static ObservableCollection<T> SortCollection<T>(ObservableCollection<T> items, PropertyInfo propertyInfo, string sortOrder)
        {
            IEnumerable<T> sortedItems;
            if (sortOrder == "ASC")
            {
                sortedItems = items.OrderBy(item => propertyInfo.GetValue(item, null));
            }
            else
            {
                sortedItems = items.OrderByDescending(item => propertyInfo.GetValue(item, null));
            }
            return new ObservableCollection<T>(sortedItems);
        }
    }
}
