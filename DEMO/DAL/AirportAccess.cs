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
                string query = "select * from AIRPORT WHERE isDeleted = 0";
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

        private string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from AIRPORT", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            i++;
            return i.ToString("000");
        }
        public string AddAirport(string airportName)
        {
            SqlConnection con = SqlConnectionData.Connect();
            try
            {
                con.Open();
                string query = "INSERT INTO AIRPORT VALUES(@AirportID, @AirportName, 0)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@AirportID", AutoID());
                    command.Parameters.AddWithValue("@AirportName", airportName);
                    command.ExecuteNonQuery();
                    state = "Success";
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return state;
        }

        public int DeleteAirport(string ID)
        {
            SqlConnection con = SqlConnectionData.Connect();
            int rowsAffected = 0;
            this.state = string.Empty;
            try
            {
                con.Open();
                string query = @"update AIRPORT
                            set isDeleted = 1
                            where isDeleted = 0
                            AND AirportID = @ID";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@ID", ID);
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
        public string GetState()
        {
            return state;
        }
    }
}