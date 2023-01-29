using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class LichChieuModel
    {
        public class LichChieuBase
        {
            public int Id { get; set; }
            public int SuatChieuId { get; set; }
            public int PhongId { get; set; }
            public int PhimId { get; set; }
            public DateTime NgayChieu { get; set; }
        }
        public class Input
        {
            public class DanhSachLichChieuTheoRap
            {
                public int RapId { get; set; }
            }
            public class DanhSachLichChieuTheoPhimVaNgay
            {
                public int PhimId { get; set; }
                public DateTime NgayChieu { get; set; }
            }
            public class DocThongTinLichChieu
            {
                public int Id { get; set; }
            }
            public class ThongTinLichChieu : LichChieuBase { }
            public class XoaLichChieu
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinLichChieu : LichChieuBase
            {
                public SuatChieuModel.Output.ThongTinSuatChieu SuatChieu { get; set; }
                public PhongModel.Output.ThongTinPhong Phong { get; set; }
                public PhimModel.Output.ThongTinPhim Phim { get; set; }
                public ThongTinLichChieu()
                {
                    SuatChieu = new();
                    Phong = new();
                    Phim = new();
                }
            }
        }
    }
}
