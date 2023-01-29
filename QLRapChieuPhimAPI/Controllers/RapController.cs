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
    public class RapController : ControllerBase
    {
        private readonly IXuLyRap _iXuLyRap;
        public RapController(IXuLyRap iXuLyRap)
        {
            _iXuLyRap = iXuLyRap;
        }

        [HttpPost("DanhSachRap")]
        public List<RapModel.Output.ThongTinRap> DanhSachRap()
        {
            var raps = _iXuLyRap.DocDanhSachRap();
            var dsRap = raps.Select(s => new RapModel.Output.ThongTinRap
            {
                Id = s.Id,
                TenRap = s.TenRap,
                DiaChi = s.DiaChi,
                DienThoai = s.DienThoai,
                Email = s.Email,
                KinhDo = s.KinhDo,
                ViDo = s.ViDo,
                NguoiQuanLyId = s.NguoiQuanLyId
            }).ToList();
            return dsRap;
        }
        [HttpPost("ThongTinRap")]
        public RapModel.Output.ThongTinRap DocThongTinRap(RapModel.Input.DocThongTinRap input)
        {
            var rap = _iXuLyRap.DocThongTinRap(input.Id);
            var thongTinRap = new RapModel.Output.ThongTinRap();
            Utilities.PropertyCopier<Rap, RapModel.Output.ThongTinRap>.Copy(rap, thongTinRap);
            return thongTinRap;
        }
        [HttpPost("ThemRap")]
        public ThongBaoModel ThemRap(RapModel.Input.ThongTinRap rap)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var rapMoi = new Rap();
                Utilities.PropertyCopier<RapModel.Input.ThongTinRap, Rap>.Copy(rap, rapMoi);
                var r = _iXuLyRap.Them(rapMoi);
                if (r != null)
                    tb.NoiDung = r.Id.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không thêm được rạp mới";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không thêm được rạp mới. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("CapNhatRap")]
        public ThongBaoModel CapNhatRap(RapModel.Input.ThongTinRap rap)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var rapSua = new Rap();
                Utilities.PropertyCopier<RapModel.Input.ThongTinRap, Rap>.Copy(rap, rapSua);
                if (!_iXuLyRap.Sua(rapSua))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được thông tin rạp.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không cập nhật được thông tin rạp. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaRap")]
        public ThongBaoModel XoaRap(RapModel.Input.XoaRap input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var rapXoa = _iXuLyRap.DocThongTinRap(input.Id);
                if (rapXoa == null || !_iXuLyRap.Xoa(rapXoa))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được thông tin rạp.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không xóa được thông tin rạp. " + ex.Message;
            }
            return tb;
        }
    }
}
