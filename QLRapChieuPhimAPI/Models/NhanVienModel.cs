using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class NhanVienModel
    {
        public class NhanVienBase
        {
            public int Id { get; set; }
            public string HoTen { get; set; }
            public bool GioiTinh { get; set; }
            public DateTime NgaySinh { get; set; }
            public string DiaChi { get; set; }
            public string Cmnd { get; set; }
            public string MatKhau { get; set; }
            public int RapId { get; set; }
            public string QuyenHan { get; set; }
        }
        public class Input
        {
            public class ThongTinDangNhap
            {
                public string TenDangNhap { get; set; }
                public string MatKhau { get; set; }
            }
            public class ThongTinThayDoiMatKhau
            {
                public int Id { get; set; }
                public string Username { get; set; }
                public string MatKhauCu { get; set; }
                public string MatKhauMoi { get; set; }
            }
            public class DanhSachNhanVien
            {
                public bool QuanTri { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinNhanVien : NhanVienBase
            {
                public string AccessToken { get; set; }
                public string ThongBao { get; set; }
            }
        }
    }
}
