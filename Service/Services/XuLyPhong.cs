using Service.Base;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class XuLyPhong : BaseRepository<Phong>, IXuLyPhong
    {        
        public XuLyPhong(QLRapChieuPhimContext context) : base(context) { }

        public List<Phong> DocDanhSachPhongTheoRap(int RapId)
        {
            return DocTheoDieuKien(x=>x.RapId.Equals(RapId), x => x.Rap).ToList();
        }

        public Phong DocThongTinPhong(int PhongId)
        {
            return DocTheoDieuKien(x => x.Id.Equals(PhongId), x => x.Rap).FirstOrDefault();
        }

        public new bool Xoa(Phong entity)
        {
            try
            {
                var ghe = _context.Ghes.FirstOrDefault(x => x.PhongId.Equals(entity.Id));
                if (ghe != null) return false;
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
