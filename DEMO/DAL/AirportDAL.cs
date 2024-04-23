using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;

namespace DAL
{
    public class AirportDAL
    {
        private static string connectionString = @"Data Source=SPIDEY;Initial Catalog=airplan_database;Integrated Security=True";

        public List<AirportDTO> GetAirports()
        {
            List<AirportDTO> airports = new List<AirportDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AirportID, AirportName FROM Airport";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        AirportDTO airport = new AirportDTO
                        {
                            AirportID = reader["AirportID"].ToString(),
                            AirportName = reader["AirportName"].ToString()
                        };

                        airports.Add(airport);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return airports;
        }
    }
}
