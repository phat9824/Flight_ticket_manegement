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
                string query = @"SELECT AirportCount, DepartureTime, IntermediateAirportCount, MinStopTime, MaxStopTime, TicketClassCount, SlowestBookingTime, CancelTime
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
            DeleteParamater();
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();
            using (SqlTransaction transaction = con.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO PARAMETER (AirportCount, DepartureTime, IntermediateAirportCount, MinStopTime, MaxStopTime, TicketClassCount, TicketClassCount, SlowestBookingTime, CancelTime, isDeleted)" +
                                        " VALUES (@AirportCount, @DepartureTime, @IntermediateAirportCount, @MinStopTime, @MaxStopTime, @TicketClassCount, @SlowestBookingTime, @CancelTime, 0);";
                        cmd.Connection = con;
                        cmd.Transaction = transaction;

                        SqlParameter parAirportCount = new SqlParameter("@AirportCount", SqlDbType.Int)
                        {
                            Value = parameter.AirportCount
                        };
                        SqlParameter parDepartureTime = new SqlParameter("@DepartureTime", SqlDbType.Time)
                        {
                            Value = parameter.DepartureTime
                        };
                        SqlParameter parIntermediateAirportCount = new SqlParameter("@IntermediateAirportCount", SqlDbType.Int)
                        {
                            Value = parameter.IntermediateAirportCount
                        };
                        SqlParameter parMinStopTime = new SqlParameter("@MinStopTime", SqlDbType.Int)
                        {
                            Value = parameter.MinStopTime
                        };
                        SqlParameter parMaxStopTime = new SqlParameter("@MaxStopTime", SqlDbType.Int)
                        {
                            Value = parameter.MaxStopTime
                        };
                        SqlParameter parTicketClassCount = new SqlParameter("@TicketClassCount", SqlDbType.Int)
                        {
                            Value = parameter.TicketClassCount
                        };
                        SqlParameter parSlowestBookingTime = new SqlParameter("@SlowestBookingTime", SqlDbType.Time)
                        {
                            Value = parameter.SlowestBookingTime
                        };
                        SqlParameter parCancelTime = new SqlParameter("@CancelTime", SqlDbType.Time)
                        {
                            Value = parameter.CancelTime
                        };

                        cmd.Parameters.Add(parAirportCount);
                        cmd.Parameters.Add(parDepartureTime);
                        cmd.Parameters.Add(parIntermediateAirportCount);
                        cmd.Parameters.Add(parMaxStopTime);
                        cmd.Parameters.Add(parTicketClassCount);
                        cmd.Parameters.Add(parSlowestBookingTime);
                        cmd.Parameters.Add(parCancelTime);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            return "No rows were inserted.";
                        }
                    }
                    transaction.Commit();
                    return string.Empty; // Chuỗi rỗng xem như thành công
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Insert failed: {ex.Message}";
                }
            }
        }
    }
}
