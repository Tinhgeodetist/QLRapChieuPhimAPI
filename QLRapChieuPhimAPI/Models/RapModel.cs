using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Models
{
    public class RapModel
    {
        public class RapBase
        {
            public int Id { get; set; }
            public string TenRap { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public int NguoiQuanLyId { get; set; }
            public double ViDo { get; set; }
            public double KinhDo { get; set; }
        }
        public class Input {
            public class ThongTinRap : RapBase { }
            public class DocThongTinRap
            {
                public int Id { get; set; }
            }
            public class XoaRap
            {
                public int Id { get; set; }
            }
        }
        public class Output
        {
            public class ThongTinRap : RapBase
            {                
            }
        }
    }
}
