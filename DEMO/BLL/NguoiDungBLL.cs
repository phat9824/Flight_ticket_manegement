using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using DAL;

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

    }
}
