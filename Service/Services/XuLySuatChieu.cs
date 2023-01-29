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
    public class XuLySuatChieu : BaseRepository<SuatChieu>, IXuLySuatChieu
    {
        public XuLySuatChieu(QLRapChieuPhimContext context) : base(context) { }

        public List<SuatChieu> DocDanhSachSuatChieu()
        {
            var dsSuatChieu = DocDanhSach().ToList();
            return dsSuatChieu;
        }

        public SuatChieu DocThongTinSuatChieu(int SuatChieuId)
        {
            var suatChieu = DocTheoDieuKien(x => x.Id.Equals(SuatChieuId)).FirstOrDefault();
            return suatChieu;
        }
    }
}
