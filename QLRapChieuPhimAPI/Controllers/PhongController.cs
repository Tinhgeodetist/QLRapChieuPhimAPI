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
    public class PhongController : ControllerBase
    {
        private readonly IXuLyPhong _iXuLyPhong;
        public PhongController(IXuLyPhong iXuLyPhong)
        {
            _iXuLyPhong = iXuLyPhong;
        }
        [HttpPost("DanhSachPhongTheoRap")]
        public List<PhongModel.Output.ThongTinPhong> DanhSachPhongTheoRap(PhongModel.Input.DanhSachPhongTheoRap input)
        {
            var dsPhong = _iXuLyPhong.DocDanhSachPhongTheoRap(input.RapId)
                                    .Select(p => new PhongModel.Output.ThongTinPhong { 
                                        Id = p.Id,
                                        TenPhong = p.TenPhong,
                                        SoLuongGhe = p.SoLuongGhe,
                                        GhiChu = p.GhiChu,
                                        RapId = p.RapId,
                                        TongSoDay = p.TongSoDay,
                                        TongSoHang = p.TongSoHang,
                                        Rap = new PhongModel.RapInfo
                                        {
                                            Id = p.Rap.Id,
                                            TenRap = p.Rap.TenRap,
                                            DiaChi = p.Rap.DiaChi,
                                            DienThoai = p.Rap.DienThoai,
                                            Email = p.Rap.Email,
                                            NguoiQuanLyId = p.Rap.NguoiQuanLyId,
                                            KinhDo = p.Rap.KinhDo,
                                            ViDo = p.Rap.ViDo
                                        }
                                    }).ToList();
            return dsPhong;
        }
        [HttpPost("ThongTinPhong")]
        public PhongModel.Output.ThongTinPhong DocThongTinPhong(PhongModel.Input.DocThongTinPhong input)
        {
            var phong = _iXuLyPhong.DocThongTinPhong(input.Id);
            if (phong == null) return new PhongModel.Output.ThongTinPhong();
            var thongTinPhong = new PhongModel.Output.ThongTinPhong
            {
                Id = phong.Id,
                TenPhong = phong.TenPhong,
                SoLuongGhe = phong.SoLuongGhe,
                GhiChu = phong.GhiChu,
                RapId = phong.RapId,
                TongSoDay = phong.TongSoDay,
                TongSoHang = phong.TongSoHang,
                Rap = new PhongModel.RapInfo
                {
                    Id = phong.Rap.Id,
                    TenRap = phong.Rap.TenRap,
                    DiaChi = phong.Rap.DiaChi,
                    DienThoai = phong.Rap.DienThoai,
                    Email = phong.Rap.Email,
                    NguoiQuanLyId = phong.Rap.NguoiQuanLyId,
                    KinhDo = phong.Rap.KinhDo,
                    ViDo = phong.Rap.ViDo
                }
            };
            return thongTinPhong;
        }
        [HttpPost("ThemPhong")]
        public ThongBaoModel ThemPhong(PhongModel.Input.ThongTinPhong phong)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var phongMoi = new Phong();
                Utilities.PropertyCopier<PhongModel.Input.ThongTinPhong, Phong>.Copy(phong, phongMoi);
                var ph = _iXuLyPhong.Them(phongMoi);
                if (ph != null)
                    tb.NoiDung = ph.Id.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không thêm được phòng mới";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không thêm được phòng mới. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("CapNhatPhong")]
        public ThongBaoModel CapNhatPhong(PhongModel.Input.ThongTinPhong phong)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var phongSua = new Phong();
                Utilities.PropertyCopier<PhongModel.Input.ThongTinPhong, Phong>.Copy(phong, phongSua);
                if(!_iXuLyPhong.Sua(phongSua))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được thông tin phòng.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không cập nhật được thông tin phòng. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaPhong")]
        public ThongBaoModel XoaPhong(PhongModel.Input.XoaPhong input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var phongXoa = _iXuLyPhong.DocThongTinPhong(input.Id);
                if (phongXoa == null || !_iXuLyPhong.Xoa(phongXoa))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được thông tin phòng.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không xóa được thông tin phòng. " + ex.Message;
            }
            return tb;
        }
    }
}
