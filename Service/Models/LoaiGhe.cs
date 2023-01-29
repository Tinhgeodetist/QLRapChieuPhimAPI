using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class LoaiGhe
    {
        public LoaiGhe()
        {
            Ghes = new HashSet<Ghe>();
            ThongTinGhes = new HashSet<ThongTinGhe>();
        }

        public int Id { get; set; }
        public string TenLoaiGhe { get; set; }
        public int GiaVe { get; set; }

        public virtual ICollection<Ghe> Ghes { get; set; }
        public virtual ICollection<ThongTinGhe> ThongTinGhes { get; set; }
    }
}
