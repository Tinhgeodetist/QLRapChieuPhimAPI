using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyGhe : IBaseRepository<Ghe>
    {
        List<Ghe> DocDanhSachGheTheoPhong(int PhongId);
        List<Ghe> DocDanhSachGheTheoPhongvaLoaiGhe(int PhongId, int LoaiGheId);
        List<Ghe> DocDanhSachGheTheoDanhSachId(List<int> dsId);
        bool PhatSinhGheTheoPhong(List<ThongTinGhe> dsThongTinGhe);
    }
}
