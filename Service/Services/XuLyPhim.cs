using Microsoft.EntityFrameworkCore;
using Service.Base;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class XuLyPhim : BaseRepository<Phim>, IXuLyPhim
    {
        public XuLyPhim(QLRapChieuPhimContext context) : base(context) { }

        public List<Phim> DocDanhSachPhimSapChieu(int SoTuanChieu = 3)
        {
            int SoNgayChieu = SoTuanChieu * 21;
            var dsPhimSapChieu = DocTheoDieuKien(x => x.NgayKhoiChieu != null &&
                                            x.NgayKhoiChieu > DateTime.Today, x => x.XepHangPhim).ToList();
            return dsPhimSapChieu;
        }

        public List<Phim> DocDanhSachPhimDangChieu(int SoTuanChieu = 3)
        {
            int SoNgayChieu = SoTuanChieu * 7;
            var dsPhimDangChieu = DocTheoDieuKien(x => x.NgayKhoiChieu != null && (x.NgayKhoiChieu <= DateTime.Today && 
                                    x.NgayKhoiChieu.Value.AddDays(SoNgayChieu) >= DateTime.Today), x => x.XepHangPhim).ToList();
            return dsPhimDangChieu;
        }

        public List<Phim> DocDanhSachPhimTheoTheLoai(int TheLoaiPhimId, 
                                        int currentPage, int pageSize = 20)
        {
            var theLoais = _context.TheLoaiPhims.ToList();
            List<Phim> dsPhimTheoTheLoai = null;
            if (TheLoaiPhimId > 0)
            {
                var theLoai = "," + TheLoaiPhimId.ToString() + ",";
                dsPhimTheoTheLoai = DocTheoDieuKien(x => x.DanhSachTheLoaiId.Contains(theLoai), x => x.XepHangPhim).ToList();
            }
            else
            {
                dsPhimTheoTheLoai = DocDanhSach().ToList();
            }

            if (pageSize <= 0)
                pageSize = 20;
            float numberpage = (float)dsPhimTheoTheLoai.Count() / pageSize;
            int pageCount = (int)Math.Ceiling(numberpage);

            if (currentPage > pageCount) currentPage = pageCount;

            dsPhimTheoTheLoai = dsPhimTheoTheLoai
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize).ToList();
            return dsPhimTheoTheLoai;
        }

        public Phim DocThongTinPhim(int id)
        {
            var phim = DocTheoDieuKien(x => x.Id.Equals(id), x => x.XepHangPhim).FirstOrDefault();
            return phim;
        }

        public new bool Xoa(Phim entity)
        {
            try
            {
                var lichChieu = _context.LichChieus.FirstOrDefault(x => x.PhimId.Equals(entity.Id));
                if (lichChieu != null) return false;
                base.Xoa(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
