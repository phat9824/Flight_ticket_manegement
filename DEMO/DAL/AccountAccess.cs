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
        public int SignUp(ACCOUNT User)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into ACCOUNT values(@ID, @name, @SDT, @Email, @Birtday, @pass, @permission)";

            SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            SqlParameter parName = new SqlParameter("@name", SqlDbType.NVarChar, 40);
            SqlParameter parSdt = new SqlParameter("@SDT", SqlDbType.Int);
            SqlParameter parMail = new SqlParameter("@Email", SqlDbType.VarChar, 40);
            SqlParameter parBirDay = new SqlParameter("@Birtday", SqlDbType.SmallDateTime);
            SqlParameter parPass = new SqlParameter("@pass", SqlDbType.VarChar, 40);
            SqlParameter parPer = new SqlParameter("@permission", SqlDbType.Int);

            parID.Value = AutoID();
            parName.Value = User.UserName;
            parSdt.Value = User.Phone;
            parMail.Value = User.Email;
            parBirDay.Value = User.Birth;
            parPass.Value = User.PasswordUser;
            parPer.Value = User.PermissonID;

            cmd.Parameters.Add(parID);
            cmd.Parameters.Add(parName);
            cmd.Parameters.Add(parSdt);
            cmd.Parameters.Add(parMail);
            cmd.Parameters.Add(parBirDay);
            cmd.Parameters.Add(parPass);
            cmd.Parameters.Add(parPer);

            cmd.Connection = con;
            int kq = cmd.ExecuteNonQuery();
            con.Close();
            return kq;
        }
    }
}
