﻿using System;
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
<<<<<<< HEAD
            //string strcon = @"Data Source=HUNG;Initial Catalog=airplanedb;Integrated Security=True";
            //string strcon = @"Data Source=LAPTOP-978A4PM7;Initial Catalog=airplanedb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string strcon = @"Data Source=SPIDEY;Initial Catalog=airplanedb;Integrated Security=True";
=======
            string strcon = @"Data Source=HUNG;Initial Catalog=airplanedb;Integrated Security=True";
            //string strcon = @"Data Source=LAPTOP-978A4PM7;Initial Catalog=airplanedb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            //string strcon = @"Data Source=SPIDEY;Initial Catalog=airplanedb;Integrated Security=True";
            //string strcon = @"Data Source=PHUONGVY\SQLEXPRESS;Initial Catalog=airplanedb;Integrated Security=True";
>>>>>>> 289e9cae80a14b48352438917d2eda4b7434c4be
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
    }
}
