using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DAL
{
    public class AccountAccess : DatabaseAccess
    {

        public string CheckLogic(ACCOUNT acc)
        {
            string info = CheckLogicDTO(acc);
            return info;
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
                        cmd.CommandText = "insert into ACCOUNT values(@ID, @Name, @SDT, @Email, @Birth, @Pass, @Permission)";
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
                            Value = User.PasswordUser
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
    }
}
