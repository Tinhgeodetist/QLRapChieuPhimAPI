using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyPhim : IBaseRepository<Phim>
    {
        List<Phim> DocDanhSachPhimDangChieu(int SoTuanChieu = 3);

        List<Phim> DocDanhSachPhimSapChieu(int SoTuanChieu = 3);

        List<Phim> DocDanhSachPhimTheoTheLoai(int TheLoaiPhimId, int currentPage, int pageSize = 20);

        Phim DocThongTinPhim(int id);

    }
}
