using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using DTO;
using System.Security.Principal;



namespace DAL
{
    public class SqlConnectionData
    {
        public static SqlConnection Connect()
        {
            //string strcon = @"Data Source=HUNG;Initial Catalog=airplanedb;Integrated Security=True";
            string strcon = @"Data Source=LAPTOP-978A4PM7;Initial Catalog=airplanedb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            //string strcon = @"Data Source=SPIDEY;Initial Catalog=airplanedb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            SqlConnection conn = new SqlConnection(strcon); // khởi tạo connect
            return conn;
        }
    }
    public class DatabaseAccess
    {
        // Method to open a connection
        private static SqlConnection OpenConnection()
        {
            SqlConnection conn = SqlConnectionData.Connect();
            conn.Open();
            return conn;
        }

        public static string CheckLogicDTO(ACCOUNT acc)
        {
            string user = null;

            // Establishing connection to the database
            using (SqlConnection conn = OpenConnection())
            {
                // Creating SQL command with parameters
                string query = "SELECT Email FROM ACCOUNT WHERE Email = @user AND PasswordUser = @pass";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@user", acc.Email);
                    command.Parameters.AddWithValue("@pass", acc.PasswordUser);

                    // Executing the SQL query
                    // Executing the SQL query and retrieving a single value
                    object result = command.ExecuteScalar();

                    // Checking if the result is not null and converting it to string
                    if (result != null)
                    {
                        user = result.ToString();
                    }
                }
            }

            if (user != null)
            {
                return user;
            }
            else
            {
                return "Tai khoan hoac mat khau khong chinh xac!";
            }
        }
    }
}
