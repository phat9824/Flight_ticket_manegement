using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Cryptography; //MD5
using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DAL
{
    public class AccountAccess : DatabaseAccess
    {
        string state = string.Empty;
        static string ToMD5Hash (string s)
        {
            StringBuilder sb = new StringBuilder ();
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5HashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                foreach (byte b in md5HashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString ();
        }
        public bool CheckAccountExists(string email)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "SELECT COUNT(*) FROM ACCOUNT WHERE Email = @email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        public int GetPermissionID(string email, string password)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "SELECT PermissionID FROM ACCOUNT WHERE Email = @email AND PasswordUser = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);
                //cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@password", ToMD5Hash(password));
                
                conn.Open();
                object result = cmd.ExecuteScalar();

                return result != null ? (int)result : 0;
            }
        }
        private string AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from ACCOUNT", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return (i + 1).ToString("000");
        }
        public string SignUp(ACCOUNT User)
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
                        cmd.CommandText = "insert into ACCOUNT values(@ID, @Name, @SDT, @Email, @Birth, @Pass, @Permission, 0)";
                        cmd.Connection = con;
                        cmd.Transaction = transaction;
                        SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20)
                        {
                            Value = AutoID()
                        };
                        SqlParameter parName = new SqlParameter("@Name", SqlDbType.NVarChar, 40)
                        {
                            Value = User.UserName
                        };
                        SqlParameter parSDT = new SqlParameter("@SDT", SqlDbType.VarChar, 20)
                        {
                            Value = User.Phone
                        };
                        SqlParameter parEmail = new SqlParameter("@Email", SqlDbType.VarChar, 60)
                        {
                            Value = User.Email
                        };
                        SqlParameter parBirth = new SqlParameter("@Birth", SqlDbType.SmallDateTime)
                        {
                            Value = User.Birth
                        };
                        SqlParameter parPass = new SqlParameter("@Pass", SqlDbType.VarChar, 60)
                        {
                            Value = ToMD5Hash(User.PasswordUser)
                        };
                        SqlParameter parPer = new SqlParameter("@Permission", SqlDbType.Int)
                        {
                            Value = User.PermissonID
                        };
                        cmd.Parameters.Add(parID);
                        cmd.Parameters.Add(parName);
                        cmd.Parameters.Add(parSDT);
                        cmd.Parameters.Add(parEmail);
                        cmd.Parameters.Add(parBirth);
                        cmd.Parameters.Add(parPass);
                        cmd.Parameters.Add(parPer);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            return "No rows were inserted.";
                        }
                        transaction.Commit();
                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Insert failed: {ex.Message}";
                }
            }
        }
        public List<ACCOUNT> GetMember(ACCOUNT dto)
        {
            List<ACCOUNT> data = new List<ACCOUNT>();
            SqlConnection con = SqlConnectionData.Connect();
            this.state = string.Empty;
            try
            {
                con.Open();
                string query = @"SELECT UserID, UserName, Phone, Email, Birth, PasswordUser, PermissionID
                                FROM ACCOUNT
                                where isDeleted = 0
                                AND (@UserID IS NULL OR @UserID = UserID)
                                AND (@UserName IS NULL OR @UserName = UserName)
                                AND (@Email IS NULL OR @Email = Email)
                                AND (@Phone IS NULL OR @Phone = Phone)";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@UserID", dto.UserID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@UserName", dto.UserName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", dto.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", dto.Phone ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ACCOUNT account = new ACCOUNT()
                            {
                                UserID = reader["UserID"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                Birth = Convert.ToDateTime(reader["Email"]),
                                PasswordUser = reader["PasswordUser"].ToString(),
                                PermissonID = Convert.ToInt32(reader["PermissionID"]),
                                IsDeleted = 0
                            };data.Add(account);
                        }
                    }
                }
            }
            catch (SqlException ex) 
            {
                state = $"Error: {ex.Message}";
            }
            con.Close();
            return data;
        }
        public string GetState()
        {
            return this.state;
        }
    }
}
