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
    public class XuLyVe : BaseRepository<Ve>, IXuLyVe
    {
        public XuLyVe(QLRapChieuPhimContext context) : base(context) { }

        public bool CapNhatThongTinVe(Ve ve)
        {
            return false;
        }

        public List<Ve> DocDanhSachVeKhongDuocMuaTheoLichChieu(int LichChieuId)
        {
            return DocTheoDieuKien(x => x.LichChieuId.Equals(LichChieuId) && x.TinhTrang != 0).ToList();
        }

        public List<Ve> DocDanhSachVeTheoLichChieu(int LichChieuId)
        {
            return DocTheoDieuKien(x => x.LichChieuId.Equals(LichChieuId), x => x.LichChieu, x => x.Ghe, x => x.NhanVien).ToList();
        }

        public Ve DocThongTinVe(int VeId)
        {
            return DocTheoDieuKien(x => x.Id.Equals(VeId), x => x.LichChieu, x => x.Ghe, x => x.NhanVien).FirstOrDefault();
        }

        public bool KiemTraVeMuaHopLe(List<Ve> dsVe)
        {
            try
            {
                if (dsVe.Count == 0) return false;
                foreach(var vemua in dsVe)
                {
                    var ve = DocTheoDieuKien(x => x.GheId.Equals(vemua.GheId) && x.LichChieuId.Equals(vemua.LichChieuId)
                                                && (x.TinhTrang.Equals(1) || x.TinhTrang.Equals(2))).FirstOrDefault();
                    if(ve != null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MuaVe(List<Ve> dsVe)
        {
            try
            {
                if (KiemTraVeMuaHopLe(dsVe))
                {
                    _context.Ves.AddRange(dsVe);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
