using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class ThanhVienModel
    {
        public class ThanhVienBase
        {
            public int Id { get; set; }
            public string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public string Email { get; set; }
            public string DienThoai { get; set; }
            public string MatKhau { get; set; }
            public bool KichHoat { get; set; }
            public string SocialLogin { get; set; }
        }
        public class Input
        {
            public class ThongTinThanhVienMoi : ThanhVienBase { }
            public class ThongTinThanhVien
            {
                public int Id { get; set; }
            }
            public class ThongTinThayDoiMatKhau
            {
                public int Id { get; set; }
                public string Username { get; set; }
                public string MatKhauCu { get; set; }
                public string MatKhauMoi { get; set; }
            }
            public class ThongTinDangNhap
            {
                public string TenDangNhap { get; set; }
                public string MatKhau { get; set; }
            }
            public class KichHoatTaiKhoan
            {
                public string Email { get; set; }
            }
        }
        public class Output
        {            
            public class ThongTinThanhVien : ThanhVienBase
            {
                public string AccessToken { get; set; }
                public string ThongBao { get; set; }                
            }
        }
    }
}
