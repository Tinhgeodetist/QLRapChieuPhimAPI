using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyLichChieu : IBaseRepository<LichChieu>
    {
        List<LichChieu> DocDanhSachLichChieuTheoRap(int RapId);
        List<LichChieu> DocDanhSachLichChieuTheoSuatChieu(int SuatChieuId);
        LichChieu DocThongTinLichChieu(int LichChieuId);
        List<LichChieu> DocDanhSachLichChieuTheoPhimVaNgay(int PhimId, DateTime NgayChieu);
    }
}
