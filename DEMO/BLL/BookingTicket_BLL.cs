using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;

namespace BLL
{
    public class BookingTicket_BLL
    {
        public DateTime GetBookingTicket_DepartureTime(string TicketID)
        {
            return new BookingTicketAccess().GetBookingTicket_DepartureTime(TicketID);
        }
    }
}
