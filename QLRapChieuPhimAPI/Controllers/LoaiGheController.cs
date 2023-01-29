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
    public class LoaiGheController : ControllerBase
    {
        private readonly IXuLyLoaiGhe _iXuLyLoaiGhe;
        public LoaiGheController(IXuLyLoaiGhe iXuLyLoaiGhe)
        {
            _iXuLyLoaiGhe = iXuLyLoaiGhe;
        }

        [HttpPost("DanhSachLoaiGhe")]
        public List<LoaiGheModel.Output.ThongTinLoaiGhe> DanhSachLoaiGhe()
        {
            var dsLoaiGhe = _iXuLyLoaiGhe.DocDanhSachLoaiGhe()
                .Select(x=> new LoaiGheModel.Output.ThongTinLoaiGhe { 
                    Id = x.Id,
                    TenLoaiGhe = x.TenLoaiGhe,
                    GiaVe = x.GiaVe
                }).ToList();
            return dsLoaiGhe;
        }

        [HttpPost("ThongTinLoaiGhe")]
        public LoaiGheModel.Output.ThongTinLoaiGhe ThongTinLoaiGhe(LoaiGheModel.Input.DocThongTinLoaiGhe input)
        {
            var loaiGhe = _iXuLyLoaiGhe.DocThongTinLoaiGhe(input.Id);
            var thongTin = new LoaiGheModel.Output.ThongTinLoaiGhe
            {
                Id = loaiGhe.Id,
                TenLoaiGhe = loaiGhe.TenLoaiGhe,
                GiaVe = loaiGhe.GiaVe
            };
            return thongTin;
        }

        [HttpPost("ThemLoaiGhe")]
        public ThongBaoModel ThemLoaiGhe(LoaiGheModel.Input.ThongTinLoaiGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var thongTinMoi = new LoaiGhe();
                Utilities.PropertyCopier<LoaiGheModel.Input.ThongTinLoaiGhe, LoaiGhe>.Copy(input, thongTinMoi);
                var tt = _iXuLyLoaiGhe.Them(thongTinMoi);
                if (tt != null)
                    tb.NoiDung = tt.Id.ToString();
                else
                {
                    tb.MaSo = 5;
                    tb.NoiDung = "Không thêm được loại ghế mới";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không thêm được loại ghế mới. " + ex.Message;
            }
            return tb;
        }

        [HttpPost("CapNhatLoaiGhe")]
        public ThongBaoModel CapNhatLoaiGhe(LoaiGheModel.Input.ThongTinLoaiGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var thongTinSua = new LoaiGhe();
                Utilities.PropertyCopier<LoaiGheModel.Input.ThongTinLoaiGhe, LoaiGhe>.Copy(input, thongTinSua);
                if (!_iXuLyLoaiGhe.Sua(thongTinSua))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được loại ghế.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không cập nhật được loại ghế. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaLoaiGhe")]
        public ThongBaoModel XoaLoaiGhe(LoaiGheModel.Input.XoaLoaiGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var thongTinXoa = _iXuLyLoaiGhe.DocThongTinLoaiGhe(input.Id);
                if (thongTinXoa == null || !_iXuLyLoaiGhe.Xoa(thongTinXoa))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được loại ghế.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không xóa được loại ghế. " + ex.Message;
            }
            return tb;
        }
    }
}
