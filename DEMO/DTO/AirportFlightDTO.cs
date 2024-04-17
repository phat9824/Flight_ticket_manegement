using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AirportFlightDTO
    {
        private string airportID;
        private string flightID;

        public string AirportID
        {
            get => airportID;
            set => airportID = value;
        }

        public string FlightID
        {
            get => flightID;
            set => flightID = value;
        }
    }
}
