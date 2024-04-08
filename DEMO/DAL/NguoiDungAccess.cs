using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;

namespace DAL
{
    public class NguoiDungAccess : DatabaseAccess
    {
        public string CheckLogic(NguoiDung nguoidung)
        {
            string info = CheckLogicDTO(nguoidung);
            return info;
        }
    }
}
