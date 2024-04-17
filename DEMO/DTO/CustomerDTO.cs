using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerDTO
    {
        private string id;
        private string customerName;
        private int phone;
        private string email;
        private DateTime birth;
        private string flightID;
        private string airportID;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string CustomerName
        {
            get => customerName;
            set => customerName = value;
        }

        public int Phone
        {
            get => phone;
            set => phone = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public DateTime Birth
        {
            get => birth;
            set => birth = value;
        }

        public string FlightID
        {
            get => flightID;
            set => flightID = value;
        }

        public string AirportID
        {
            get => airportID;
            set => airportID = value;
        }
    }

}
