using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class ThongTinGheModel
    {
        public class ThongTinGheBase
        {
            public int Id { get; set; }
            public string KyHieu { get; set; }
            public int SoGhe { get; set; }
            public int Day { get; set; }
            public int Hang { get; set; }
            public int PhongId { get; set; }
            public int LoaiGheId { get; set; }
            public string ViTriGheVip { get; set; }
        }
        public class Input
        {
            public class DanhSachThongTinGheTheoPhong
            {
                public int PhongId { get; set; }
            }
            public class DocThongTinGhe
            {
                public int Id { get; set; }
            }
            public class ThongTinGhe : ThongTinGheBase { }
            public class XoaThongTinGhe
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinGhe : ThongTinGheBase
            {
                public PhongInfo Phong { get; set; }
                public LoaiGheInfo LoaiGhe { get; set; }
                public ThongTinGhe()
                {
                    Phong = new();
                }
            }
        }
        public class PhongInfo
        {
            public int Id { get; set; }
            public string TenPhong { get; set; }
            public int SoLuongGhe { get; set; }
            public string GhiChu { get; set; }
            public int RapId { get; set; }
            public int TongSoDay { get; set; }
            public int TongSoHang { get; set; }
        }
        public class LoaiGheInfo
        {
            public int Id { get; set; }
            public string TenLoaiGhe { get; set; }
            public int GiaVe { get; set; }
        }
    }
}
