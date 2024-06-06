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
            string query = @"SELECT TicketClassID, TicketClassName, BaseMultiplier
                             FROM TICKET_CLASS
                             WHERE isDeleted = 0";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                // Thiết lập các tham số

                // Đọc kết quả truy vấn
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ticketclass.Add(new TicketClassDTO()
                        {
                            TicketClassID = (string)reader["TicketClassID"],
                            TicketClassName = (string)reader["TicketClassName"],
                            BaseMultiplier = (decimal)(double)reader["BaseMultiplier"]
                        });
                    }
                }
            }
            // Đóng kết nối
            con.Close();

            return ticketclass;
        }
        public int getTotalSeat_byFlightID(string flightID)
        {
            int number = 0;
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();
            string query = @"SELECT ISNULL(SUM(Quantity), 0) AS TotalSeat
                            FROM TICKETCLASS_FLIGHT
                            WHERE (@flightID IS NULL OR FlightID = @flightID)
                            AND isDeleted = 0";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                // Thiết lập các tham số
                command.Parameters.AddWithValue("@flightID", flightID ?? (object)DBNull.Value);

                // Đọc kết quả truy vấn
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        number = (int)reader["TotalSeat"];
                    }
                }
            }
            // Đóng kết nối
            con.Close();

            return number;
        }

        public int getTotalSeat_byFlightID_TicketClassID(string flightID, string ticketClassID)
        {
            int number = 0;
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();
            string query = @"SELECT ISNULL(SUM(Quantity), 0) AS TotalSeat
                            FROM TICKETCLASS_FLIGHT
                            WHERE (@flightID IS NULL OR FlightID = @flightID)
                            AND (@ticketClassID IS NULL OR TicketClassID = @ticketClassID)
                            AND isDeleted = 0";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                // Thiết lập các tham số
                command.Parameters.AddWithValue("@flightID", flightID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ticketClassID", ticketClassID ?? (object)DBNull.Value);

                // Đọc kết quả truy vấn
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        number = (int)reader["TotalSeat"];
                    }
                }
            }
            // Đóng kết nối
            con.Close();

            return number;
        }
    }
}
