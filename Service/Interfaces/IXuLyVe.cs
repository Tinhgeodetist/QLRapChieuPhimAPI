using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyVe : IBaseRepository<Ve>
    {
        List<Ve> DocDanhSachVeTheoLichChieu(int LichChieuId);
        List<Ve> DocDanhSachVeKhongDuocMuaTheoLichChieu(int LichChieuId);
        Ve DocThongTinVe(int VeId);
        bool KiemTraVeMuaHopLe(List<Ve> dsVe);
        bool MuaVe(List<Ve> dsVe);
        bool CapNhatThongTinVe(Ve ve);
    }
}
