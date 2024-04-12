using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    
    public class ACCOUNT_BLL
    {
        AccountAccess accAccess = new AccountAccess();
        public string CheckLogic(ACCOUNT acc)
        {
            if (acc.Email == "") 
            {
                return "required tk_email";
            }
            if (acc.Email == "")
            {
                return "required pass";
            }

            string info = accAccess.CheckLogic(acc);
            return info;

        }
        public int AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from ACCOUNT", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return i + 1;
        }
        public void SignUp(ACCOUNT User, ref int kq)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into ACCOUNT values(@ID, @name, @SDT, @Email, @Birtday, @pass, '1')";

            SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            SqlParameter parName = new SqlParameter("@name", SqlDbType.VarChar, 40);
            SqlParameter parSdt = new SqlParameter("@SDT", SqlDbType.Int);
            SqlParameter parMail = new SqlParameter("@Email", SqlDbType.VarChar, 40);
            SqlParameter parBirDay = new SqlParameter("@Birtday", SqlDbType.SmallDateTime);
            SqlParameter parPass = new SqlParameter("@pass", SqlDbType.VarChar, 40);

            parID.Value = AutoID();
            parName.Value = User.UserName;
            parSdt.Value = User.Phone;
            parMail.Value = User.Email;
            parBirDay.Value = User.Birth;
            parPass.Value = User.PasswordUser;

            cmd.Parameters.Add(parID);
            cmd.Parameters.Add(parName);
            cmd.Parameters.Add(parSdt);
            cmd.Parameters.Add(parMail);
            cmd.Parameters.Add(parBirDay);
            cmd.Parameters.Add(parPass);

            cmd.Connection = con;
            kq = cmd.ExecuteNonQuery();
        }

    }
}
