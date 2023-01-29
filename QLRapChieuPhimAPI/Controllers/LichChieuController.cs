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
    public class LichChieuController : ControllerBase
    {
        private readonly IXuLyLichChieu _iXuLyLichChieu;
        private readonly IXuLyRap _iXuLyRap;
        public LichChieuController(IXuLyLichChieu iXuLyLichChieu, IXuLyRap iXuLyRap)
        {
            _iXuLyLichChieu = iXuLyLichChieu;
            _iXuLyRap = iXuLyRap;
        }
        [HttpPost("DanhSachLichChieuTheoRap")]
        public List<LichChieuModel.Output.ThongTinLichChieu> DanhSachLichChieuTheoRap(LichChieuModel.Input.DanhSachLichChieuTheoRap input)
        {
            var dsLichChieu = _iXuLyLichChieu.DocDanhSachLichChieuTheoRap(input.RapId)
                                    .Select(x => new LichChieuModel.Output.ThongTinLichChieu
                                    {
                                        Id = x.Id,
                                        SuatChieuId = x.SuatChieuId,
                                        PhongId = x.PhongId,
                                        PhimId = x.PhimId,
                                        NgayChieu = x.NgayChieu,
                                        SuatChieu = new SuatChieuModel.Output.ThongTinSuatChieu
                                        {
                                            Id = x.SuatChieu.Id,
                                            TenSuatChieu = x.SuatChieu.TenSuatChieu,
                                            GioBatDau = x.SuatChieu.GioBatDau,
                                            GioKetThuc = x.SuatChieu.GioKetThuc
                                        },
                                        Phong = new PhongModel.Output.ThongTinPhong
                                        {
                                            Id = x.Phong.Id,
                                            TenPhong = x.Phong.TenPhong
                                        },
                                        Phim = new PhimModel.Output.ThongTinPhim
                                        {
                                            Id = x.Phim.Id,
                                            TenPhim = x.Phim.TenPhim
                                        }
                                    }).ToList();
            return dsLichChieu;
        }
        [HttpPost("DanhSachLichChieuTheoPhimVaNgay")]
        public List<LichChieuModel.Output.ThongTinLichChieu> DanhSachLichChieuTheoPhimVaNgay(LichChieuModel.Input.DanhSachLichChieuTheoPhimVaNgay input)
        {
            var dsLichChieu = _iXuLyLichChieu.DocDanhSachLichChieuTheoPhimVaNgay(input.PhimId, input.NgayChieu)
                                    .Select(x => new LichChieuModel.Output.ThongTinLichChieu
                                    {
                                        Id = x.Id,
                                        SuatChieuId = x.SuatChieuId,
                                        PhongId = x.PhongId,
                                        PhimId = x.PhimId,
                                        NgayChieu = x.NgayChieu,
                                        SuatChieu = new SuatChieuModel.Output.ThongTinSuatChieu
                                        {
                                            Id = x.SuatChieu.Id,
                                            TenSuatChieu = x.SuatChieu.TenSuatChieu,
                                            GioBatDau = x.SuatChieu.GioBatDau,
                                            GioKetThuc = x.SuatChieu.GioKetThuc
                                        },
                                        Phong = new PhongModel.Output.ThongTinPhong
                                        {
                                            Id = x.Phong.Id,
                                            TenPhong = x.Phong.TenPhong,
                                            RapId = x.Phong.RapId
                                        },
                                        Phim = new PhimModel.Output.ThongTinPhim
                                        {
                                            Id = x.Phim.Id,
                                            TenPhim = x.Phim.TenPhim
                                        }
                                    }).ToList();
            var dsRap = _iXuLyRap.DocDanhSachRap();
            foreach(var lich in dsLichChieu)
            {
                var rap = dsRap.FirstOrDefault(x => x.Id.Equals(lich.Phong.RapId));
                if(rap != null)
                {
                    lich.Phong.Rap = new PhongModel.RapInfo
                    {
                        Id = rap.Id,
                        TenRap = rap.TenRap,
                        KinhDo = rap.KinhDo,
                        ViDo = rap.ViDo
                    };
                }
            }
            return dsLichChieu;
        }
        [HttpPost("ThongTinLichChieu")]
        public LichChieuModel.Output.ThongTinLichChieu DocThongTinLichChieu(LichChieuModel.Input.DocThongTinLichChieu input)
        {
            var lichchieu = _iXuLyLichChieu.DocThongTinLichChieu(input.Id);
            var thongTinLichChieu = new LichChieuModel.Output.ThongTinLichChieu
            {
                Id = lichchieu.Id,
                SuatChieuId = lichchieu.SuatChieuId,
                PhongId = lichchieu.PhongId,
                PhimId = lichchieu.PhimId,
                NgayChieu = lichchieu.NgayChieu,
                SuatChieu = new SuatChieuModel.Output.ThongTinSuatChieu
                {
                    Id = lichchieu.SuatChieu.Id,
                    TenSuatChieu = lichchieu.SuatChieu.TenSuatChieu,
                    GioBatDau = lichchieu.SuatChieu.GioBatDau,
                    GioKetThuc = lichchieu.SuatChieu.GioKetThuc
                },
                Phong = new PhongModel.Output.ThongTinPhong
                {
                    Id = lichchieu.Phong.Id,
                    TenPhong = lichchieu.Phong.TenPhong,
                    SoLuongGhe = lichchieu.Phong.SoLuongGhe,
                    TongSoDay = lichchieu.Phong.TongSoDay,
                    TongSoHang = lichchieu.Phong.TongSoHang
                },
                Phim = new PhimModel.Output.ThongTinPhim
                {
                    Id = lichchieu.Phim.Id,
                    TenPhim = lichchieu.Phim.TenPhim,
                    ThoiLuong = lichchieu.Phim.ThoiLuong,
                    XepHangPhimId = lichchieu.Phim.XepHangPhimId
                }
            };
            return thongTinLichChieu;
        }
        [HttpPost("ThemLichChieu")]
        public ThongBaoModel ThemLichChieu(LichChieuModel.Input.ThongTinLichChieu lichchieu)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var lichchieuMoi = new LichChieu();
                Utilities.PropertyCopier<LichChieuModel.Input.ThongTinLichChieu, LichChieu>.Copy(lichchieu, lichchieuMoi);
                var lc = _iXuLyLichChieu.Them(lichchieuMoi);
                if (lc != null)
                    tb.NoiDung = lc.Id.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không thêm được lịch chiếu mới";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không thêm được lịch chiếu mới. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaLichChieu")]
        public ThongBaoModel XoaLichChieu(LichChieuModel.Input.XoaLichChieu input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var lichchieuXoa = _iXuLyLichChieu.DocThongTinLichChieu(input.Id);
                if (lichchieuXoa == null || !_iXuLyLichChieu.Xoa(lichchieuXoa))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được thông tin lịch chiếu.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không xóa được thông tin lịch chiếu. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("CapNhatLichChieu")]
        public ThongBaoModel CapNhatLichChieu(LichChieuModel.Input.ThongTinLichChieu lichchieu)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var lichchieuSua = new LichChieu();
                Utilities.PropertyCopier<LichChieuModel.Input.ThongTinLichChieu, LichChieu>.Copy(lichchieu, lichchieuSua);
                if (!_iXuLyLichChieu.Sua(lichchieuSua))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được thông tin lịch chiếu.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không cập nhật được thông tin lịch chiếu. " + ex.Message;
            }
            return tb;
        }
    }
}
