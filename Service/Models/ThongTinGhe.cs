using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class ThongTinGhe
    {
        public int Id { get; set; }
        public string KyHieu { get; set; }
        public int SoGhe { get; set; }
        public int Day { get; set; }
        public int Hang { get; set; }
        public int PhongId { get; set; }
        public int LoaiGheId { get; set; }
        public string ViTriGheVip { get; set; }

        public virtual LoaiGhe LoaiGhe { get; set; }
        public virtual Phong Phong { get; set; }
    }
}
