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
    public class XuLyXepHangPhim : BaseRepository<XepHangPhim>, IXuLyXepHangPhim
    {
        public XuLyXepHangPhim(QLRapChieuPhimContext context) : base(context) { }
        public List<XepHangPhim> DocDanhSachXepHangPhim()
        {
            var dsXepHang = DocDanhSach().ToList();
            return dsXepHang;
        }

        public XepHangPhim DocThongTinXepHangPhim(int id)
        {
            var xepHang = DocTheoDieuKien(x => x.Id.Equals(id)).FirstOrDefault();
            return xepHang;
        }

        public new bool Xoa(XepHangPhim entity)
        {
            try
            {
                var phim = _context.Phims.FirstOrDefault(x => x.XepHangPhimId.Equals(entity.Id));
                if (phim != null) return false;
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
