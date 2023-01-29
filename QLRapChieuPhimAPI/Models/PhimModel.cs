using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class PhimModel {
        public class PhimBase {
            public int Id { get; set; }
            public string TenPhim { get; set; }
            public string TenGoc { get; set; }
            public int ThoiLuong { get; set; }
            public string DaoDien { get; set; }
            public string DienVien { get; set; }
            public DateTime NgayKhoiChieu { get; set; }
            public string NoiDung { get; set; }
            public string NuocSanXuat { get; set; }
            public string NhaSanXuat { get; set; }
            public string Poster { get; set; }
            public string DanhSachTheLoaiId { get; set; }
            public string NgonNgu { get; set; }
            public int XepHangPhimId { get; set; }
            public string Trailer { get; set; }
        }
        public class Input
        {
            public class ThongTinPhim : PhimBase { }
            public class DocThongTinPhim
            {
                public int PhimId { get; set; }
            }
            public class XoaPhim : DocThongTinPhim { }
            public class TimThongTinPhim
            {
                public string TuKhoa { get; set; }
                public int PageSize { get; set; }
                public int CurrentPage { get; set; }
            }
            public class PhimTheoTheLoai
            {
                public int TheLoaiId { get; set; }
                public int PageSize { get; set; }
                public int CurrentPage { get; set; }
            }
        }
        
        public class Output
        {
            public class ThongTinPhim : PhimBase {
                public XepHangPhimModel.XepHangPhimBase XepHangPhim { get; set; }
                public List<TheLoaiPhimModel.TheLoaiPhimBase> DanhSachTheLoai { get; set; }

                public ThongTinPhim() {
                    XepHangPhim = new XepHangPhimModel.XepHangPhimBase();
                    DanhSachTheLoai = new List<TheLoaiPhimModel.TheLoaiPhimBase>();
                }
            }

            public class PhimTheoTheLoai
            {
                public TheLoaiPhimModel.TheLoaiPhimBase TheLoaiHienHanh { get; set; }
                public List<TheLoaiPhimModel.TheLoaiPhimBase> DanhSachTheLoai { get; set; }
                public List<ThongTinPhim> DanhSachPhim { get; set; }
                public int CurrentPage { get; set; }
                public int PageCount { get; set; }
                public PhimTheoTheLoai()
                {
                    TheLoaiHienHanh = new TheLoaiPhimModel.TheLoaiPhimBase();
                    DanhSachPhim = new List<ThongTinPhim>();
                    DanhSachTheLoai = new List<TheLoaiPhimModel.TheLoaiPhimBase>();
                }
            }
            public class ThemPhimMoi : PhimBase
            {
                public List<XepHangPhimModel.XepHangPhimBase> DanhSachXepHangPhim { get; set; }
                public List<TheLoaiPhimModel.TheLoaiPhimBase> DanhSachTheLoai { get; set; }
                public string ThongBao { get; set; }
                public ThemPhimMoi()
                {
                    DanhSachTheLoai = new List<TheLoaiPhimModel.TheLoaiPhimBase>();
                    DanhSachXepHangPhim = new List<XepHangPhimModel.XepHangPhimBase>();
                }
            }
            public class CapNhatPhim : PhimBase
            {
                public List<XepHangPhimModel.XepHangPhimBase> DanhSachXepHangPhim { get; set; }
                public List<TheLoaiPhimModel.TheLoaiPhimBase> DanhSachTheLoai { get; set; }
                public string ThongBao { get; set; }
                public CapNhatPhim()
                {
                    DanhSachTheLoai = new List<TheLoaiPhimModel.TheLoaiPhimBase>();
                    DanhSachXepHangPhim = new List<XepHangPhimModel.XepHangPhimBase>();
                }
            }
        }
    }
}
