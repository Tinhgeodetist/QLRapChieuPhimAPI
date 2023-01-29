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
    public class XuLyTheLoaiPhim : BaseRepository<TheLoaiPhim>, IXuLyTheLoaiPhim
    {
        public XuLyTheLoaiPhim(QLRapChieuPhimContext context) : base(context) { }
        public List<TheLoaiPhim> DocDanhSachTheLoai()
        {
            var dsTheLoai = DocDanhSach().ToList();
            return dsTheLoai;
        }
        public TheLoaiPhim DocThongTinTheLoai(int id)
        {
            var theLoai = DocTheoDieuKien(x=>x.Id.Equals(id)).FirstOrDefault();
            return theLoai;
        }

        public new bool Xoa(TheLoaiPhim entity)
        {
            try {
                var theloaiid = "," + entity.Id.ToString() + ",";
                var phim = _context.Phims
                                   .FirstOrDefault(x => x.DanhSachTheLoaiId.Equals(theloaiid));
                if (phim != null) return false;
                base.Xoa(entity);
                return true;

            }
            catch (Exception) {
                return false;
            }
        }
    }
}
