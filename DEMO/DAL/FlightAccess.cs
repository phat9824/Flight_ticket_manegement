using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Runtime.CompilerServices;
using System.Collections;

namespace DAL
{
    public class FlightAccess : DatabaseAccess
    {   
        string state = string.Empty; // Chuỗi rỗng xem như thành công
        public string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from FLIGHT", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            i++;
            return i.ToString("000");
        }

        /*public void Add_Flights(FlightDTO flight)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into FLIGHT values(@ID, @SouID, @DesID, @FlDay, @FlTime, @price)";

            SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            SqlParameter parSouID = new SqlParameter("@SouID", SqlDbType.VarChar, 20);
            SqlParameter parDesID = new SqlParameter("@DesID", SqlDbType.VarChar, 20);
            SqlParameter parFlDay = new SqlParameter("@FlDay", SqlDbType.SmallDateTime);
            SqlParameter parFlTime = new SqlParameter("@FlTime", SqlDbType.Time);
            SqlParameter parPrice = new SqlParameter("@price", SqlDbType.Money);

            parID.Value = AutoID();
            parSouID.Value = flight.SourceAirportID;
            parDesID.Value = flight.DestinationAirportID;
            parFlDay.Value = flight.FlightDay.Add(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            parFlTime.Value = flight.FlightTime;
            parPrice.Value = flight.Price;

            cmd.Parameters.Add(parID);
            cmd.Parameters.Add(parSouID);
            cmd.Parameters.Add(parDesID);
            cmd.Parameters.Add(parFlDay);
            cmd.Parameters.Add(parFlTime);
            cmd.Parameters.Add(parPrice);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }*/

        // Cũng là Add_Flight nhưng có trả về trạng thái xử lý để dễ debug bằng cách huyền thoại
        public string Add_Flights(FlightDTO flight)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            using (SqlTransaction transaction = con.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO FLIGHT (FlightID, SourceAirportID, DestinationAirportID, FlightDay, FlightTime, Price) VALUES (@ID, @SouID, @DesID, @FlDay, @FlTime, @Price)";
                        cmd.Connection = con;
                        cmd.Transaction = transaction;

                        SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar)
                        {
                            Value = new FlightAccess().AutoID()
                        };
                        SqlParameter parSouID = new SqlParameter("@SouID", SqlDbType.VarChar)
                        {
                            Value = flight.SourceAirportID
                        };
                        SqlParameter parDesID = new SqlParameter("@DesID", SqlDbType.VarChar)
                        {
                            Value = flight.DestinationAirportID
                        };
                        SqlParameter parFlDay = new SqlParameter("@FlDay", SqlDbType.SmallDateTime)
                        {
                            Value = flight.FlightDay
                        };
                        SqlParameter parFlTime = new SqlParameter("@FlTime", SqlDbType.Time)
                        {
                            Value = flight.FlightTime
                        };
                        SqlParameter parPrice = new SqlParameter("@Price", SqlDbType.Money)
                        {
                            Value = flight.Price
                        };

                        cmd.Parameters.Add(parID);
                        cmd.Parameters.Add(parSouID);
                        cmd.Parameters.Add(parDesID);
                        cmd.Parameters.Add(parFlDay);
                        cmd.Parameters.Add(parFlTime);
                        cmd.Parameters.Add(parPrice);

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

        public List<FlightDTO> getFlight(string sourceAirportID, string destinationAirportID, DateTime startDate, DateTime endDate)
        {
            List<FlightDTO> data = new List<FlightDTO>();
            SqlConnection con = SqlConnectionData.Connect();
            this.state = string.Empty; 
            try
            {
                con.Open();
                string query = @"SELECT FlightID, SourceAirportID, DestinationAirportID, FlightDay, FlightTime, Price
                                FROM FLIGHT
                                WHERE (@sourceAirportID IS NULL OR SourceAirportID = @sourceAirportID)
                                AND (@destinationAirportID IS NULL OR DestinationAirportID = @destinationAirportID)
                                AND FlightDay BETWEEN @startDate AND @endDate";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // Thiết lập các tham số
                    command.Parameters.AddWithValue("@sourceAirportID", sourceAirportID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@destinationAirportID", destinationAirportID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    // Đọc kết quả truy vấn
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightDTO flight = new FlightDTO()
                            {
                                FlightID = reader["FlightID"].ToString(),
                                DestinationAirportID = reader["DestinationAirportID"].ToString(),
                                SourceAirportID = reader["SourceAirportID"].ToString(),
                                FlightDay = Convert.ToDateTime(reader["FlightDay"]),
                                FlightTime = (TimeSpan)reader["FlightTime"],
                                Price = Convert.ToDecimal(reader["Price"])

                            };
                            data.Add(flight);
                        }
                    }
                }
            } catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            // Đóng kết nối
            con.Close();
            return data;
        }

        public List<FlightDTO> getFlight(string sourceAirportID, string destinationAirportID, DateTime startDate, DateTime endDate, string ticketClass, int numTicket)
        {
            List<FlightDTO> data = new List<FlightDTO>();
            SqlConnection con = SqlConnectionData.Connect();
            string state = string.Empty;
            try
            {
                con.Open();

                string query = @"
                                SELECT f.FlightID, f.SourceAirportID, f.DestinationAirportID, f.FlightDay, f.FlightTime, f.Price
                                FROM FLIGHT f
                                INNER JOIN TICKETCLASS_FLIGHT tf ON f.FlightID = tf.FlightID
                                LEFT JOIN (
                                    SELECT FlightID, TicketClassID, COUNT(*) AS BookedTickets
                                    FROM BOOKING_TICKET
                                    GROUP BY FlightID, TicketClassID
                                ) bt ON f.FlightID = bt.FlightID AND tf.TicketClassID = bt.TicketClassID
                                WHERE (@sourceAirportID IS NULL OR f.SourceAirportID = @sourceAirportID)
                                AND (@destinationAirportID IS NULL OR f.DestinationAirportID = @destinationAirportID)
                                AND f.FlightDay BETWEEN @startDate AND @endDate
                                AND (@ticketClass IS NULL OR tf.TicketClassID = @ticketClass)
                                AND (tf.Quantity - ISNULL(bt.BookedTickets, 0)) >= @numTicket";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    // Thiết lập các tham số
                    command.Parameters.AddWithValue("@sourceAirportID", sourceAirportID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@destinationAirportID", destinationAirportID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);
                    command.Parameters.AddWithValue("@ticketClass", ticketClass ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@numTicket", numTicket);

                    // Đọc kết quả truy vấn
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightDTO flight = new FlightDTO()
                            {
                                FlightID = reader["FlightID"].ToString(),
                                SourceAirportID = reader["SourceAirportID"].ToString(),
                                DestinationAirportID = reader["DestinationAirportID"].ToString(),
                                FlightDay = Convert.ToDateTime(reader["FlightDay"]),
                                FlightTime = (TimeSpan)reader["FlightTime"],
                                Price = Convert.ToDecimal(reader["Price"])
                            };
                            data.Add(flight);
                        }
                    }
                }
            } catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            // Đóng kết nối
            con.Close();
            return data;
        }

        public string GetState()
        {
            return this.state;
        }
    }
}
