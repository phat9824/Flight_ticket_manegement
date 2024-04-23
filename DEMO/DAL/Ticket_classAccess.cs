using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DAL
{
    public class Ticket_classAccess : DatabaseAccess
    {
        public List<TicketClassDTO> L_TicketClass()
        {
            List<TicketClassDTO> ticketclass = new List<TicketClassDTO>();
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from TICKET_CLASS";

            cmd.Connection = con;

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ticketclass.Add(new TicketClassDTO() { TicketClassID = rdr.GetString(0), TicketClassName = rdr.GetString(1) });
            }
            rdr.Close();
            con.Close();

            return ticketclass;
        }
    }
}
