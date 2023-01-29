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
    public class XuLyLoaiGhe : BaseRepository<LoaiGhe>, IXuLyLoaiGhe
    {
        public XuLyLoaiGhe(QLRapChieuPhimContext context) : base(context) { }

        public List<LoaiGhe> DocDanhSachLoaiGhe()
        {
            var dsLoaiGhe = DocDanhSach().ToList();
            return dsLoaiGhe;
        }

        public LoaiGhe DocThongTinLoaiGhe(int Id)
        {
            var loaiGhe = DocTheoDieuKien(x => x.Id.Equals(Id)).FirstOrDefault();
            return loaiGhe;
        }
    }
}
