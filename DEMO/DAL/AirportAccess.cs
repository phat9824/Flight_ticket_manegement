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
    public class AirportAccess : DatabaseAccess
    {
        string state = string.Empty;
        public List<AirportDTO> L_airport()
        {
            List<AirportDTO> data = new List<AirportDTO>();
            SqlConnection con = SqlConnectionData.Connect();
            try
            {
                con.Open();
                string query = "select * from AIRPORT";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            AirportDTO airport = new AirportDTO() 
                            {
                                AirportID = reader[0].ToString(),
                                AirportName = reader[1].ToString(),
                            };
                            data.Add(airport);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return data;
        }
        /*public List<AirportDTO> L_airport()
        {
            List<AirportDTO> airports = new List<AirportDTO>();
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from AIRPORT";

            cmd.Connection = con;

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                airports.Add(new AirportDTO() { AirportID = rdr.GetString(0), AirportName = rdr.GetString(1) });
            }
            rdr.Close();
            con.Close();

            return airports;
        }*/
    }
}
