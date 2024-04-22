using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ACCOUNT
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; } // Dùng DateTime? để cho phép giá trị null
        public string PasswordUser { get; set; }
        public int PermissonID { get; set; }
    }
}
