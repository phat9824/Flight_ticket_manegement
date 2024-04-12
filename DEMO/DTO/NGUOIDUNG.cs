using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDung
    {
        public string MaDangNhap { get; set; }
        public string TenNguoiDung { get; set; }
        public int SoDienThoai { get; set; } // Dùng int? để cho phép giá trị null
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; } // Dùng DateTime? để cho phép giá trị null
        public string PasswordND { get; set; }
        //public int MaQuyen { get; set; }

        // Mô hình có thể bao gồm các thuộc tính khác tùy thuộc vào yêu cầu của ứng dụng
    }
}
