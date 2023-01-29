using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class PhongModel
    {
        public class PhongBase
        {
            public int Id { get; set; }
            public string TenPhong { get; set; }
            public int SoLuongGhe { get; set; }
            public string GhiChu { get; set; }
            public int RapId { get; set; }
            public int TongSoDay { get; set; }
            public int TongSoHang { get; set; }
        }
        public class Input {
            public class DanhSachPhongTheoRap
            {
                public int RapId { get; set; }
            }
            public class DocThongTinPhong
            {
                public int Id { get; set; }
            }
            public class ThongTinPhong : PhongBase { }
            public class XoaPhong
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinPhong : PhongBase
            {
                public RapInfo Rap { get; set; }
                public ThongTinPhong()
                {
                    Rap = new();
                }
            }
        }
        public class RapInfo
        {
            public int Id { get; set; }
            public string TenRap { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public int NguoiQuanLyId { get; set; }
            public double ViDo { get; set; }
            public double KinhDo { get; set; }
        }
    }
}
