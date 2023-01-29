using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLySuatChieu : IBaseRepository<SuatChieu>
    {
        List<SuatChieu> DocDanhSachSuatChieu();
        SuatChieu DocThongTinSuatChieu(int SuatChieuId);
    }
}
