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
    public class XuLySlideBanner : BaseRepository<SlideBanner>, IXuLySlideBanner
    {
        public XuLySlideBanner(QLRapChieuPhimContext context) : base(context) { }

        public List<SlideBanner> DocDanhSachSlideBanner(bool quantri = false)
        {
            List<SlideBanner> slideBanners;
            if (quantri)
                slideBanners = DocDanhSach().ToList();
            else
                slideBanners = DocTheoDieuKien(x => x.KichHoat).ToList();
            return slideBanners;
        }

        public SlideBanner DocThongTinSlideBanner(int id)
        {
            var slideBanner = DocTheoDieuKien(x => x.Id.Equals(id)).FirstOrDefault();
            return slideBanner;
        }

    }
}
