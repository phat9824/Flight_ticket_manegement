using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FlightInformationSearchDTO
    {
        public FlightDTO Flight {  get; set; }

        public int bookedTickets { get; set; }
        public int totalTickets { get; set; }
        public List<IntermediateAirportDTO> IntermediateAirports { get; set;}
        public List<TicketClassFlightDTO> TicketClasses { get; set; }
    }
}
