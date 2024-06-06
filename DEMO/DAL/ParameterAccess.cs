using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ParameterAccess : DatabaseAccess
    {
        string state = string.Empty;
        public ParameterDTO GetParameters()
        {
            ParameterDTO parameter = new ParameterDTO();

            SqlConnection con = SqlConnectionData.Connect();
            try
            {
                con.Open();
                string query = @"SELECT AirportCount, MinFlighTime, IntermediateAirportCount, MinStopTime, MaxStopTime, TicketClassCount, SlowestBookingTime, CancelTime
                             FROM PARAMETER
                             where isDeleted = 0";

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
                                MinFlighTime = (TimeSpan)reader["MinFlighTime"],
                                IntermediateAirportCount = Convert.ToInt32(reader["IntermediateAirportCount"]),
                                MinStopTime = (TimeSpan)reader["MinStopTime"],
                                MaxStopTime = (TimeSpan)reader["MaxStopTime"],
                                TicketClassCount = Convert.ToInt32(reader["TicketClassCount"]),
                                SlowestBookingTime = (TimeSpan)reader["SlowestBookingTime"],
                                CancelTime = (TimeSpan)reader["CancelTime"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            // Đóng kết nối
            con.Close();
            return parameter;
        }
        public int DeleteParamater()
        {
            SqlConnection con = SqlConnectionData.Connect();
            int rowsAffected = 0;
            this.state = string.Empty;
            try
            {
                con.Open();
                string query = @"update PARAMETER
                            set isDeleted = 1
                            where isDeleted = 0";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return rowsAffected;
        }
        public string InsertParamater(ParameterDTO parameter)
        {

            return "";
        }
    }
}
