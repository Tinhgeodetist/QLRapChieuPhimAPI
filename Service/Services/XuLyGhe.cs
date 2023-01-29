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
    public class XuLyGhe : BaseRepository<Ghe>, IXuLyGhe
    {
        public XuLyGhe(QLRapChieuPhimContext context) : base(context) { }

        public List<Ghe> DocDanhSachGheTheoDanhSachId(List<int> dsId)
        {
            var dsGhe = DocTheoDieuKien(x => dsId.Contains(x.Id), x => x.Phong, XepHangPhim => XepHangPhim.LoaiGhe).ToList();
            return dsGhe;
        }

        public List<Ghe> DocDanhSachGheTheoPhong(int PhongId)
        {
            var dsGhe = DocTheoDieuKien(x => x.PhongId.Equals(PhongId), x => x.Phong, XepHangPhim => XepHangPhim.LoaiGhe).ToList();
            return dsGhe;
        }

        public List<Ghe> DocDanhSachGheTheoPhongvaLoaiGhe(int PhongId, int LoaiGheId)
        {
            var dsGhe = DocTheoDieuKien(x => x.PhongId.Equals(PhongId) && x.LoaiGheId.Equals(LoaiGheId), 
                                                        x => x.Phong, XepHangPhim => XepHangPhim.LoaiGhe).ToList();
            return dsGhe;
        }

        public bool PhatSinhGheTheoPhong(List<ThongTinGhe> dsThongTinGhe)
        {
            try
            {
                var dsGhe = new List<Ghe>();
                foreach (var tt in dsThongTinGhe)
                {
                    int vip_bd = 0, vip_kt = 0;
                    if (!string.IsNullOrEmpty(tt.ViTriGheVip))
                    {
                        var vips = tt.ViTriGheVip.Split('-');
                        vip_bd = int.Parse(vips[0]);
                        if (vips.Length == 2)
                            vip_kt = int.Parse(vips[1]);
                        else
                            vip_kt = tt.SoGhe;
                    }
                    for (int i = 1; i <= tt.SoGhe; i++)
                    {
                        var ghe = new Ghe
                        {
                            SoGhe = tt.KyHieu + i.ToString(),
                            Day = tt.Day,
                            Hang = tt.Hang + i - 1,
                            PhongId = tt.PhongId,
                            LoaiGheId = tt.LoaiGheId,
                            GheVip = (i >= vip_bd && i <= vip_kt) ? true : false
                        };
                        dsGhe.Add(ghe);
                    }
                }
                _context.Ghes.AddRange(dsGhe);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
