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
    public class XuLyRap : BaseRepository<Rap>, IXuLyRap
    {
        public XuLyRap(QLRapChieuPhimContext context) : base(context) { }
        public List<Rap> DocDanhSachRap()
        {
            var dsRap = DocDanhSach().ToList();
            return dsRap;
        }

        public Rap DocThongTinRap(int id)
        {
            var rap = DocTheoDieuKien(r => r.Id.Equals(id)).FirstOrDefault();
            return rap;
        }

        public new bool Xoa(Rap entity)
        {
            try
            {
                var phong = _context.Phongs.FirstOrDefault(p => p.RapId.Equals(entity.Id));
                if (phong != null) return false;
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
