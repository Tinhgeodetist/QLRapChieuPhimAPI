using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Ghes = new HashSet<Ghe>();
            LichChieus = new HashSet<LichChieu>();
            ThongTinGhes = new HashSet<ThongTinGhe>();
        }

        public int Id { get; set; }
        public string TenPhong { get; set; }
        public int SoLuongGhe { get; set; }
        public string GhiChu { get; set; }
        public int RapId { get; set; }
        public int TongSoDay { get; set; }
        public int TongSoHang { get; set; }

        public virtual Rap Rap { get; set; }
        public virtual ICollection<Ghe> Ghes { get; set; }
        public virtual ICollection<LichChieu> LichChieus { get; set; }
        public virtual ICollection<ThongTinGhe> ThongTinGhes { get; set; }
    }
}
