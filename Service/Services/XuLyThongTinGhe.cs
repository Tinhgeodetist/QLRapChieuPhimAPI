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
    public class XuLyThongTinGhe : BaseRepository<ThongTinGhe>, IXuLyThongTinGhe
    {
        public XuLyThongTinGhe(QLRapChieuPhimContext context) : base(context) { }

        public ThongTinGhe DocChiTietThongTinGhe(int Id)
        {
            var thongTin = DocTheoDieuKien(x => x.Id.Equals(Id), x => x.Phong, x => x.LoaiGhe).FirstOrDefault();
            return thongTin;
        }

        public List<ThongTinGhe> DocDanhSachThongTinGheTheoPhong(int PhongId)
        {
            var dsThongTin = DocTheoDieuKien(x => x.PhongId.Equals(PhongId), x => x.Phong, x => x.LoaiGhe).ToList();
            return dsThongTin;
        }
    }
}
