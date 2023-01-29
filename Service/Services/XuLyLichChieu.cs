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
    public class XuLyLichChieu : BaseRepository<LichChieu>, IXuLyLichChieu
    {
        public XuLyLichChieu(QLRapChieuPhimContext context) : base(context) { }

        public List<LichChieu> DocDanhSachLichChieuTheoPhimVaNgay(int PhimId, DateTime NgayChieu)
        {
            return DocTheoDieuKien(x => x.PhimId.Equals(PhimId) && x.NgayChieu.Equals(NgayChieu), x => x.Phong, x => x.SuatChieu, x => x.Phim).ToList();
        }

        public List<LichChieu> DocDanhSachLichChieuTheoRap(int RapId)
        {
            List<int> dsPhongId = _context.Phongs.Where(p => p.RapId.Equals(RapId)).Select(p => p.Id).ToList();
            return DocTheoDieuKien(x => dsPhongId.Contains(x.PhongId), x => x.Phong, x => x.SuatChieu, x => x.Phim).ToList();
        }

        public List<LichChieu> DocDanhSachLichChieuTheoSuatChieu(int SuatChieuId)
        {
            return DocTheoDieuKien(x => x.SuatChieuId.Equals(SuatChieuId), x => x.Phong, x => x.SuatChieu, x => x.Phim).ToList();
        }

        public LichChieu DocThongTinLichChieu(int LichChieuId)
        {
            return DocTheoDieuKien(x => x.Id.Equals(LichChieuId), x => x.Phim, x => x.Phong, x => x.SuatChieu).FirstOrDefault();
        }
    }
}
