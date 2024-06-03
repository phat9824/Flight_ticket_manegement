using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IntermidateAirportAccess
    {
        string state = string.Empty;
        public IntermidateAirportAccess() { }
        public int GetNumIntermidateAirport(string FlightID)
        {
            SqlConnection con = SqlConnectionData.Connect();
            string state = string.Empty;
            int count = 0;
            try
            {
                con.Open();
                string query = @"select *from INTERMEDIATE_AIRPORT
                                where isDeleted = 0
                                and (@FlightID is NULL or @FlightID = FlightID)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@FlightID", FlightID ?? (object)DBNull.Value);
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";

            }
            con.Close();
            return count;
        }
        public string insertListItermedateAirport(List<IntermediateAirportDTO> listIntermediateAirportDTO)
        {
            SqlConnection con = SqlConnectionData.Connect();
            
            con.Open();

            using (SqlTransaction transaction = con.BeginTransaction())
            {
                try
                {
                    StringBuilder queryBuilder = new StringBuilder("INSERT INTO INTERMEDIATE_AIRPORT (FlightID, AirportID, LayoverTime, Note, isDeleted) VALUES ");
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    for (int i = 0; i < listIntermediateAirportDTO.Count; i++)
                    {
                        IntermediateAirportDTO intermediateAirport = listIntermediateAirportDTO[i];

                        queryBuilder.Append($"(@FlightID_{i}, @AirportID_{i}, @LayoverTime_{i}, @Note_{i}, 0),");

                        SqlParameter flightIDParam = new SqlParameter($"@FlightID_{i}", System.Data.SqlDbType.VarChar);
                        flightIDParam.Value = intermediateAirport.FlightID;
                        parameters.Add(flightIDParam);

                        SqlParameter airportIDParam = new SqlParameter($"@AirportID_{i}", System.Data.SqlDbType.VarChar);
                        airportIDParam.Value = intermediateAirport.AirportID;
                        parameters.Add(airportIDParam);

                        SqlParameter layoverTimeParam = new SqlParameter($"@LayoverTime_{i}", System.Data.SqlDbType.Time);
                        layoverTimeParam.Value = intermediateAirport.LayoverTime;
                        parameters.Add(layoverTimeParam);

                        SqlParameter noteParam = new SqlParameter($"@Note_{i}", System.Data.SqlDbType.NVarChar);
                        noteParam.Value = string.IsNullOrEmpty(intermediateAirport.Note) ? (object)DBNull.Value : intermediateAirport.Note;
                        parameters.Add(noteParam);
                    }

                    if (queryBuilder.Length > 0 && queryBuilder[queryBuilder.Length - 1] == ',')
                    {
                        queryBuilder.Remove(queryBuilder.Length - 1, 1);
                    }

                    using (SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), con, transaction))
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

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
        //
        public int GetNumIntermidiateAirport()
        {
            SqlConnection con = SqlConnectionData.Connect();
            string state = string.Empty;
            int count = 0;
            try
            {
                con.Open();
                string query = @"select count(*) from INTERMEDIATE_AIRPORT";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return count;
        }
        public string GetState()
        {
            return this.state;
        }
    }
}
