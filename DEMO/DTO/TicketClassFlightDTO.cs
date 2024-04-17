using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TicketClassFlightDTO
    {
        private string ticketClassID;
        private string flightID;
        private int quantity;

        public string TicketClassID
        {
            get => ticketClassID;
            set => ticketClassID = value;
        }

        public string FlightID
        {
            get => flightID;
            set => flightID = value;
        }

        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }
    }
}
