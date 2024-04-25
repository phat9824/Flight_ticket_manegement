using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
<<<<<<< HEAD
using System.Runtime.CompilerServices;
=======
using System.Collections;
>>>>>>> 2ec5174f66589509a2857ba12d6e4e27a8020238

namespace DAL
{
    public class FlightAccess : DatabaseAccess
    {
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
        public void Add_Flights(FlightDTO flight)
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
        }
        public List<FlightDTO> getFlight(string sourceAirportID, string destinationAirportID
                                                                       , DateTime startDate, DateTime endDate)
        {
            List<FlightDTO> data = new List<FlightDTO>();
            SqlConnection con = SqlConnectionData.Connect();
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
            // Đóng kết nối
            con.Close();
            return data;
        }
    }
}
