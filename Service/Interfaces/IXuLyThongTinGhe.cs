using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyThongTinGhe : IBaseRepository<ThongTinGhe>
    {
        List<ThongTinGhe> DocDanhSachThongTinGheTheoPhong(int PhongId);
        ThongTinGhe DocChiTietThongTinGhe(int Id);
    }
}
