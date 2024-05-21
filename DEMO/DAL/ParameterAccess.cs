using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ParameterAccess : DatabaseAccess
    {
        public static ParameterDTO GetParameters()
        {
            ParameterDTO parameter = new ParameterDTO();

            SqlConnection con = SqlConnectionData.Connect();
            con.Open();
            string query = @"SELECT AirportCount, DepartureTime, IntermediateAirportCount, MinStopTime, MaxStopTime, TicketClassCount, SlowestBookingTime, CancelTime
                             FROM PARAMETER";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                // Đọc kết quả truy vấn
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parameter = new ParameterDTO()
                        {
                            AirportCount = Convert.ToInt32(reader["AirportCount"]),
                            DepartureTime = (TimeSpan)reader["DepartureTime"],
                            IntermediateAirportCount = Convert.ToInt32(reader["IntermediateAirportCount"]),
                            MinStopTime = Convert.ToInt32(reader["MinStopTime"]),
                            MaxStopTime = Convert.ToInt32(reader["MaxStopTime"]),
                            TicketClassCount = Convert.ToInt32(reader["TicketClassCount"]),
                            SlowestBookingTime = (TimeSpan)reader["SlowestBookingTime"],
                            CancelTime = (TimeSpan)reader["CancelTime"]
                        };
                    }
                }
            }
            // Đóng kết nối
            con.Close();
            return parameter;
        }
    }
}
