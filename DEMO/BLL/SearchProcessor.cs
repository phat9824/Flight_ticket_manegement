using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.SqlServer.Server;

namespace BLL
{
    public class SearchProcessor
    {
        public SearchProcessor() { }
        public List<FlightInformationSearchDTO> GetInformationSearch(string sourceAirportID, string destinationAirportID
                                                                       , DateTime startDate, DateTime endDate)
        {
            // Nhận đầu vào là các tham số cho truy vấn và thuộc tính được sort khi trả về:
            // Các thuộc tính có thể có hoặc không
            // Trả về danh sách dữ liệu theo yêu cầu
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
    }
}
