using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class Ghe
    {
        public Ghe()
        {
            Ves = new HashSet<Ve>();
        }

        public int Id { get; set; }
        public string SoGhe { get; set; }
        public int LoaiGheId { get; set; }
        public int PhongId { get; set; }
        public int Day { get; set; }
        public int Hang { get; set; }
        public bool GheVip { get; set; }

        public virtual LoaiGhe LoaiGhe { get; set; }
        public virtual Phong Phong { get; set; }
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
