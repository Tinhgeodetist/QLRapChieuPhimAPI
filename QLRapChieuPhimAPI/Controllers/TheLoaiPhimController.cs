using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhimAPI.Common;
using QLRapChieuPhimAPI.Models;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiPhimController : ControllerBase
    {
        private readonly IXuLyTheLoaiPhim _iXuLyTheLoaiPhim;
        public TheLoaiPhimController(IXuLyTheLoaiPhim iXuLyTheLoaiPhim)
        {
            _iXuLyTheLoaiPhim = iXuLyTheLoaiPhim;
        }

        [HttpPost("DanhSachTheLoai")]
        public List<TheLoaiPhimModel.Output.ThongTinTheLoaiPhim> DanhSachTheLoai()
        {
            var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
            return dsTheLoai.Select(tl => new TheLoaiPhimModel.Output.ThongTinTheLoaiPhim
            {
                Id = tl.Id,
                Ten = tl.Ten
            }).ToList();
        }
        [HttpGet]
        public List<TheLoaiPhimModel.Output.ThongTinTheLoaiPhim> DanhSach()
        {
            var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
            return dsTheLoai.Select(tl => new TheLoaiPhimModel.Output.ThongTinTheLoaiPhim
            {
                Id = tl.Id,
                Ten = tl.Ten
            }).ToList();
        }
        //Tương tự cho các phương thức khác
    }
}
