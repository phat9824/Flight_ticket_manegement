using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Flight_BLL
    {
        public string AutoID()
        {
            FlightAccess flightAccess = new FlightAccess();
            return flightAccess.AutoID();
        }
        public void Add_Flights(FlightDTO flight)
        {
            FlightAccess flightAccess = new FlightAccess();
            flightAccess.Add_Flights(flight);
        }
        
        public List<AirportDTO> L_airport()
        {
            FlightAccess flightAccess = new FlightAccess();
            return flightAccess.L_airport();
        }
        public List<TicketClassDTO> L_TicketClass()
        {
            FlightAccess flightAccess = new FlightAccess();
            return flightAccess.L_TicketClass();
        }

    }
}
