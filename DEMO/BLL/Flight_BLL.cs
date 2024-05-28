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
        public string Get_ID()
        {
            FlightAccess flight = new FlightAccess();
            return flight.AutoID();
        }
        /*public void Add_Flights(FlightDTO flight)
        {
            FlightAccess flightAccess = new FlightAccess();
            flightAccess.Add_Flights(flight);
        }*/

        public string Add_Flights(FlightDTO flight)
        {
            return new DAL.FlightAccess().Add_Flights(flight);
        }
    }
}
