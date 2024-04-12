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
    
    public class NguoiDungBLL
    {
        NguoiDungAccess ndAccess = new NguoiDungAccess();
        public string CheckLogic(NguoiDung nguoidung)
        {
            if (nguoidung.Email == "") 
            {
                return "required tk_email";
            }
            if (nguoidung.Email == "")
            {
                return "required pass";
            }

            string info = ndAccess.CheckLogic(nguoidung);
            return info;

        }
        public int AutoID()
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from NGUOIDUNG", con);
            int i = Convert.ToInt32 (cmd.ExecuteScalar());
            con.Close();
            return i + 1;
        }
        public void SignUp(NguoiDung User, ref int kq)
        {
            SqlConnection con = SqlConnectionData.Connect();
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into NGUOIDUNG values(@ID, @name, @SDT, @Email, @Birtday, @pass, '1')";

            SqlParameter parID = new SqlParameter("@ID", SqlDbType.VarChar, 20);
            SqlParameter parName = new SqlParameter("@name", SqlDbType.VarChar, 40);
            SqlParameter parSdt = new SqlParameter("@SDT", SqlDbType.Int);
            SqlParameter parMail = new SqlParameter("@Email", SqlDbType.VarChar, 40);
            SqlParameter parBirDay = new SqlParameter("@Birtday", SqlDbType.SmallDateTime);
            SqlParameter parPass = new SqlParameter("@pass", SqlDbType.VarChar, 40);

            parID.Value = AutoID();
            parName.Value = User.TenNguoiDung;
            parSdt.Value = User.SoDienThoai;
            parMail.Value = User.Email;
            parBirDay.Value = User.NgaySinh;
            parPass.Value = User.PasswordND;

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
