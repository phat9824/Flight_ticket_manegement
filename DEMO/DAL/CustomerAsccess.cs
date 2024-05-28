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
    public class CustomerAsccess : DatabaseAccess
    {
        string state = string.Empty; // Chuỗi rỗng xem như thành công
        public string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from CUSTOMER", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            i++;
            return i.ToString("CS000");
        }
        public string Add_Customer(CustomerDTO customer)
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
                        cmd.CommandText = "INSERT INTO CUSTOMER (ID, CustomerName, Phone, Email, Birth) VALUES (@ID, @Name, @Phone, @Mail, @Birth)";
                        cmd.Connection = con;
                        cmd.Transaction = transaction;

                        SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20)
                        {
                            Value = new CustomerAsccess().AutoID()
                        };
                        SqlParameter parName = new SqlParameter("@Name", SqlDbType.VarChar, 40)
                        {
                            Value = customer.CustomerName
                        };
                        SqlParameter parPhone = new SqlParameter("@Phone", SqlDbType.VarChar, 20)
                        {
                            Value = customer.Phone
                        };
                        SqlParameter parMail = new SqlParameter("@Mail", SqlDbType.VarChar, 60)
                        {
                            Value = customer.Email
                        };
                        SqlParameter parBirth = new SqlParameter("@Birth", SqlDbType.SmallDateTime)
                        {
                            Value = customer.Birth
                        };

                        cmd.Parameters.Add(parID);
                        cmd.Parameters.Add(parName);
                        cmd.Parameters.Add(parPhone);
                        cmd.Parameters.Add(parMail);
                        cmd.Parameters.Add(parBirth);

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

    }
}
