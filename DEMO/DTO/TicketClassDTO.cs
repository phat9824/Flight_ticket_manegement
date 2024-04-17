using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TicketClassDTO
    {
        private string ticketClassID;
        private string ticketClassName;

        public string TicketClassID
        {
            get => ticketClassID;
            set => ticketClassID = value;
        }

        public string TicketClassName
        {
            get => ticketClassName;
            set => ticketClassName = value;
        }
    }
}
