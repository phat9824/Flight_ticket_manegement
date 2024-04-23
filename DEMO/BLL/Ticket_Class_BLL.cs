using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Ticket_Class_BLL
    {
        public List<TicketClassDTO> L_TicketClass()
        {
            Ticket_classAccess ticket_class = new Ticket_classAccess();
            return ticket_class.L_TicketClass();
        }
    }
}
