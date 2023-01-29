using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class SuatChieuModel
    {
        public class SuatChieuBase
        {
            public int Id { get; set; }
            public string TenSuatChieu { get; set; }
            public string GioBatDau { get; set; }
            public string GioKetThuc { get; set; }
        }
        public class Input {
            public class ThongTinSuatChieu : SuatChieuBase { }
            public class DocThongTinSuatChieu
            {
                public int Id { get; set; }
            }
            public class XoaSuatChieu
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinSuatChieu : SuatChieuBase
            {
                public string ThongBao { get; set; }
            }
        }
    }
}
