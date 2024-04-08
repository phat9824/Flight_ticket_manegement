using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DTO;



namespace DAL
{

    public class SqlConnectionData
    {
        public static SqlConnection Connect()
        {
            string strcon = @"Data Source=SPIDEY;Initial Catalog=Flight_ticket_database;Integrated Security=True";
            SqlConnection conn = new SqlConnection(strcon); // khởi tạo connect
            return conn;
        }
    }
    public class DatabaseAccess
    {
        public static string CheckLogicDTO(NguoiDung nguoidung)
        {
            string user = null;
            //hàm connect tới csdl
            SqlConnection conn =  SqlConnectionData.Connect();
            conn.Open();
            SqlCommand command = new SqlCommand("proc_logic", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue ("@user", nguoidung.Email);
            command.Parameters.AddWithValue ("@pass", nguoidung.PasswordND);
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) 
            {
                while(reader.Read()) 
                {
                    user = reader.GetString (0);
                    return user;
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                return "Tai khoan hoac mat khau khong chinh xac!";
            }

            return user ;
        }
    }
}
