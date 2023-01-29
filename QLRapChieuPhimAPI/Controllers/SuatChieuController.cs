using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhimAPI.Common;
using QLRapChieuPhimAPI.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuatChieuController : ControllerBase
    {
        private readonly IXuLySuatChieu _iXuLySuatChieu;
        private readonly IXuLyLichChieu _iXuLyLichChieu;
        public SuatChieuController(IXuLySuatChieu iXuLySuatChieu, IXuLyLichChieu iXuLyLichChieu)
        {
            _iXuLySuatChieu = iXuLySuatChieu;
            _iXuLyLichChieu = iXuLyLichChieu;
        }
        [HttpPost("DanhSachSuatChieu")]
        public List<SuatChieuModel.Output.ThongTinSuatChieu> DanhSachSuatChieu()
        {
            var dsSuatChieu = _iXuLySuatChieu.DocDanhSachSuatChieu()
                                .Select(x => new SuatChieuModel.Output.ThongTinSuatChieu {
                                    Id = x.Id,
                                    TenSuatChieu = x.TenSuatChieu,
                                    GioBatDau = x.GioBatDau,
                                    GioKetThuc = x.GioKetThuc
                                }).ToList();
            return dsSuatChieu;
        }
        [HttpPost("ThongTinSuatChieu")]
        public SuatChieuModel.Output.ThongTinSuatChieu XemThongTinSuatChieu(SuatChieuModel.Input.DocThongTinSuatChieu input)
        {
            SuatChieuModel.Output.ThongTinSuatChieu thongtinSuatChieu = new();
            if (input.Id > 0)
            {
                try
                {
                    var suatChieu = _iXuLySuatChieu.DocThongTinSuatChieu(input.Id);
                    if (suatChieu != null)
                    {
                        Utilities.PropertyCopier<Service.Models.SuatChieu, SuatChieuModel.Output.ThongTinSuatChieu>.Copy(suatChieu, thongtinSuatChieu);
                    }
                    else
                        thongtinSuatChieu.ThongBao = "Lỗi: Không tìm thấy thông tin suất chiếu";
                }
                catch (Exception ex)
                {
                    thongtinSuatChieu.ThongBao = "Lỗi: " + ex.Message;
                }
            }
            else
            {
                thongtinSuatChieu.ThongBao = "Lỗi: Id Suất Chiếu không hợp lệ";
            }
            return thongtinSuatChieu;
        }
        [HttpPost("ThemSuatChieu")]
        public ThongBaoModel ThemSuatChieuMoi(SuatChieuModel.Input.ThongTinSuatChieu input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var suatChieuMoi = new Service.Models.SuatChieu();
                Utilities.PropertyCopier<SuatChieuModel.Input.ThongTinSuatChieu, Service.Models.SuatChieu>.Copy(input, suatChieuMoi);
                var suat = _iXuLySuatChieu.Them(suatChieuMoi);
                if (suat != null)
                    tb.NoiDung = suat.Id.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không thêm được Suất chiếu mới";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Lỗi thêm Suất chiếu. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("CapNhatSuatChieu")]
        public ThongBaoModel CapNhatSuatChieu(SuatChieuModel.Input.ThongTinSuatChieu input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var suatChieu = _iXuLySuatChieu.DocThongTinSuatChieu(input.Id);
                if (suatChieu != null)
                {
                    Utilities.PropertyCopier<SuatChieuModel.Input.ThongTinSuatChieu, Service.Models.SuatChieu>.Copy(input, suatChieu);
                    if(!_iXuLySuatChieu.Sua(suatChieu))
                    {
                        tb.MaSo = 1;
                        tb.NoiDung = "Không cập nhật được Suất chiếu.";
                    }

                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không cập nhật được Suất chiếu." + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaSuatChieu")]
        public ThongBaoModel XoaSuatChieu(SuatChieuModel.Input.XoaSuatChieu input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var suatChieuXoa = _iXuLySuatChieu.DocThongTinSuatChieu(input.Id);
                if (suatChieuXoa == null)
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Suất chiếu không hợp lệ.";
                }
                else
                {
                    var dslichChieu = _iXuLyLichChieu.DocDanhSachLichChieuTheoSuatChieu(suatChieuXoa.Id);
                    if(dslichChieu != null && dslichChieu.Count > 0)
                    {
                        tb.MaSo = 3;
                        tb.NoiDung = "Suất chiếu này đã xếp Lịch chiếu. Không xóa được.";
                    }
                    else if(!_iXuLySuatChieu.Xoa(suatChieuXoa))
                    {
                        tb.MaSo = 4;
                        tb.NoiDung = "Không xóa được Suất chiếu.";
                    }
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không xóa được Suất chiếu. " + ex.Message;
            }
            return tb;
        }
    }
}
