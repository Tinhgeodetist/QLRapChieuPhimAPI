using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class SlideBannerModel
    {
        public class SlideBannerBase
        {
            public int Id { get; set; }
            public string Ten { get; set; }
            public string Hinh { get; set; }
            public string LienKet { get; set; }
            public bool KichHoat { get; set; }
        }
        public class Input {
            public class ThongTinSlideBanner : SlideBannerBase { }
            public class DocDanhSachSlideBanner
            {
                public bool QuanTri { get; set; }
            }
            public class DocThongTinSlideBanner
            {
                public int Id { get; set; }
            }
            public class XoaSlideBanner
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinSlideBanner : SlideBannerBase { }
        }
    }
}
