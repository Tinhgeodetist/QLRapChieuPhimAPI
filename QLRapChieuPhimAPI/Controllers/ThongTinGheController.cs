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
    public class ThongTinGheController : ControllerBase
    {
        private readonly IXuLyThongTinGhe _iXuLyThongTinGhe;
        private readonly IXuLyPhong _iXuLyPhong;
        public ThongTinGheController(IXuLyThongTinGhe iXuLyThongTinGhe, IXuLyPhong iXuLyPhong)
        {
            _iXuLyThongTinGhe = iXuLyThongTinGhe;
            _iXuLyPhong = iXuLyPhong;
        }
        [HttpPost("DanhSachThongTinGheTheoPhong")]
        public List<ThongTinGheModel.Output.ThongTinGhe> DanhSachThongTinGheTheoPhong(ThongTinGheModel.Input.DanhSachThongTinGheTheoPhong input)
        {
            var dsThongTin = _iXuLyThongTinGhe.DocDanhSachThongTinGheTheoPhong(input.PhongId)
                .Select(x=> new ThongTinGheModel.Output.ThongTinGhe { 
                    Id = x.Id,
                    KyHieu = x.KyHieu,
                    Day = x.Day,
                    Hang = x.Hang,
                    PhongId = x.PhongId,
                    SoGhe = x.SoGhe,
                    ViTriGheVip = x.ViTriGheVip,
                    LoaiGheId = x.LoaiGheId,
                    Phong = new ThongTinGheModel.PhongInfo
                    {
                        Id = x.Phong.Id,
                        TenPhong = x.Phong.TenPhong,
                        SoLuongGhe = x.Phong.SoLuongGhe,
                        TongSoDay = x.Phong.TongSoDay,
                        RapId = x.Phong.RapId,
                        TongSoHang = x.Phong.TongSoHang,
                        GhiChu = x.Phong.GhiChu
                    },
                    LoaiGhe = new ThongTinGheModel.LoaiGheInfo
                    {
                        Id = x.LoaiGhe.Id,
                        TenLoaiGhe = x.LoaiGhe.TenLoaiGhe,
                        GiaVe = x.LoaiGhe.GiaVe
                    }
                }).ToList();
            return dsThongTin;
        }

        [HttpPost("ChiTietThongTinGhe")]
        public ThongTinGheModel.Output.ThongTinGhe ChiTietThongTinGhe(ThongTinGheModel.Input.DocThongTinGhe input)
        {
            var tt = _iXuLyThongTinGhe.DocChiTietThongTinGhe(input.Id);
            var thongTinGhe = new ThongTinGheModel.Output.ThongTinGhe
            {
                Id = tt.Id,
                KyHieu = tt.KyHieu,
                Day = tt.Day,
                Hang = tt.Hang,
                PhongId = tt.PhongId,
                SoGhe = tt.SoGhe,
                ViTriGheVip = tt.ViTriGheVip,
                LoaiGheId = tt.LoaiGheId,
                Phong = new ThongTinGheModel.PhongInfo
                {
                    Id = tt.Phong.Id,
                    TenPhong = tt.Phong.TenPhong,
                    SoLuongGhe = tt.Phong.SoLuongGhe,
                    TongSoDay = tt.Phong.TongSoDay,
                    RapId = tt.Phong.RapId,
                    TongSoHang = tt.Phong.TongSoHang,
                    GhiChu = tt.Phong.GhiChu
                },
                LoaiGhe = new ThongTinGheModel.LoaiGheInfo
                {
                    Id = tt.LoaiGhe.Id,
                    TenLoaiGhe = tt.LoaiGhe.TenLoaiGhe,
                    GiaVe = tt.LoaiGhe.GiaVe
                }
            };
            return thongTinGhe;
        }

        [HttpPost("ThemThongTinGhe")]
        public ThongBaoModel ThemThongTinGhe(ThongTinGheModel.Input.ThongTinGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var phong = _iXuLyPhong.DocThongTinPhong(input.PhongId);
                var dsThongTin = _iXuLyThongTinGhe.DocDanhSachThongTinGheTheoPhong(input.PhongId);
                var tongsoghe = dsThongTin.Sum(x => x.SoGhe);
                if(dsThongTin.Count >= phong.TongSoDay)
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Tổng số dãy ghế không được vượt quá Tổng số dãy ghế của phòng";
                }
                else if (input.SoGhe > phong.TongSoHang)
                {
                    tb.MaSo = 3;
                    tb.NoiDung = "Số ghế trong một dãy ghế không được vượt quá Tổng số ghế trong dãy của phòng";
                }
                else if (tongsoghe + input.SoGhe > phong.SoLuongGhe)
                {
                    tb.MaSo = 4;
                    tb.NoiDung = "Tổng số ghế không được vượt quá Số lượng ghế của phòng";
                }
                else
                {
                    var thongTinMoi = new ThongTinGhe();
                    Utilities.PropertyCopier<ThongTinGheModel.Input.ThongTinGhe, ThongTinGhe>.Copy(input, thongTinMoi);
                    var tt = _iXuLyThongTinGhe.Them(thongTinMoi);
                    if (tt != null)
                        tb.NoiDung = tt.Id.ToString();
                    else
                    {
                        tb.MaSo = 5;
                        tb.NoiDung = "Không thêm được Thông tin ghế";
                    }
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không thêm được Thông tin ghế. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("CapNhatThongTinGhe")]
        public ThongBaoModel CapNhatThongTinGhe(ThongTinGheModel.Input.ThongTinGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var thongTinSua = new ThongTinGhe();
                Utilities.PropertyCopier<ThongTinGheModel.Input.ThongTinGhe, ThongTinGhe>.Copy(input, thongTinSua);
                if (!_iXuLyThongTinGhe.Sua(thongTinSua))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được thông tin ghế.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không cập nhật được thông tin ghế. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("XoaThongTinGhe")]
        public ThongBaoModel XoaThongTinGhe(ThongTinGheModel.Input.XoaThongTinGhe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var thongTinXoa = _iXuLyThongTinGhe.DocChiTietThongTinGhe(input.Id);
                if (thongTinXoa == null || !_iXuLyThongTinGhe.Xoa(thongTinXoa))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được thông tin ghế.";
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không xóa được thông tin ghế. " + ex.Message;
            }
            return tb;
        }
    }
}
