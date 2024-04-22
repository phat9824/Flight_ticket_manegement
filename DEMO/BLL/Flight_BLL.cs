using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Flight_BLL
    {
        private string AutoID()
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
            parFlDay.Value = flight.FlightDay;
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
        }
    }
}
