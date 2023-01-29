using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class GheController : ControllerBase
    {
        private readonly IXuLyGhe _iXuLyGhe;
        private readonly IXuLyThongTinGhe _iXuLyThongTinGhe;
        public GheController(IXuLyGhe iXuLyGhe, IXuLyThongTinGhe iXuLyThongTinGhe)
        {
            _iXuLyGhe = iXuLyGhe;
            _iXuLyThongTinGhe = iXuLyThongTinGhe;
        }
        [HttpPost("DanhSachGheTheoPhong")]
        public List<GheModel.Output.ThongTinGhe> DanhSachGheTheoPhong(GheModel.Input.DanhSachGheTheoPhong input)
        {
            var dsGhe = _iXuLyGhe.DocDanhSachGheTheoPhong(input.PhongId)
                .Select(x => new GheModel.Output.ThongTinGhe
                {
                    Id = x.Id,
                    Day = x.Day,
                    Hang = x.Hang,
                    PhongId = x.PhongId,
                    SoGhe = x.SoGhe,
                    LoaiGheId = x.LoaiGheId,
                    GheVip = x.GheVip,
                    Phong = new GheModel.PhongInfo
                    {
                        Id = x.Phong.Id,
                        TenPhong = x.Phong.TenPhong,
                        SoLuongGhe = x.Phong.SoLuongGhe,
                        TongSoDay = x.Phong.TongSoDay,
                        RapId = x.Phong.RapId,
                        TongSoHang = x.Phong.TongSoHang,
                        GhiChu = x.Phong.GhiChu
                    },
                    LoaiGhe = new GheModel.LoaiGheInfo
                    {
                        Id = x.LoaiGhe.Id,
                        GiaVe = x.LoaiGhe.GiaVe,
                        TenLoaiGhe = x.LoaiGhe.TenLoaiGhe
                    }
                }).ToList();
            return dsGhe;
        }
        [HttpPost("DanhSachGheTheoDanhSachId")]
        public List<GheModel.Output.ThongTinGhe> DanhSachGheTheoDanhSachId(GheModel.Input.DanhSachGheTheoDanhSachId input)
        {
            if (input.DanhSachId == null || input.DanhSachId.Count == 0) return new List<GheModel.Output.ThongTinGhe>();
            var dsGhe = _iXuLyGhe.DocDanhSachGheTheoDanhSachId(input.DanhSachId)
                .Select(x => new GheModel.Output.ThongTinGhe
                {
                    Id = x.Id,
                    Day = x.Day,
                    Hang = x.Hang,
                    PhongId = x.PhongId,
                    SoGhe = x.SoGhe,
                    LoaiGheId = x.LoaiGheId,
                    GheVip = x.GheVip,
                    Phong = new GheModel.PhongInfo
                    {
                        Id = x.Phong.Id,
                        TenPhong = x.Phong.TenPhong,
                        SoLuongGhe = x.Phong.SoLuongGhe,
                        TongSoDay = x.Phong.TongSoDay,
                        RapId = x.Phong.RapId,
                        TongSoHang = x.Phong.TongSoHang,
                        GhiChu = x.Phong.GhiChu
                    },
                    LoaiGhe = new GheModel.LoaiGheInfo
                    {
                        Id = x.LoaiGhe.Id,
                        GiaVe = x.LoaiGhe.GiaVe,
                        TenLoaiGhe = x.LoaiGhe.TenLoaiGhe
                    }
                }).ToList();
            return dsGhe;
        }
        [HttpPost("DanhSachGheTheoPhongVaLoaiGhe")]
        public List<GheModel.Output.ThongTinGhe> DanhSachGheTheoPhongVaLoaiGhe(GheModel.Input.DanhSachGheTheoPhongVaLoaiGhe input)
        {
            var dsGhe = _iXuLyGhe.DocDanhSachGheTheoPhongvaLoaiGhe(input.PhongId, input.LoaiGheId)
                .Select(x => new GheModel.Output.ThongTinGhe
                {
                    Id = x.Id,
                    Day = x.Day,
                    Hang = x.Hang,
                    PhongId = x.PhongId,
                    SoGhe = x.SoGhe,
                    LoaiGheId = x.LoaiGheId,
                    GheVip = x.GheVip,
                    Phong = new GheModel.PhongInfo
                    {
                        Id = x.Phong.Id,
                        TenPhong = x.Phong.TenPhong,
                        SoLuongGhe = x.Phong.SoLuongGhe,
                        TongSoDay = x.Phong.TongSoDay,
                        RapId = x.Phong.RapId,
                        TongSoHang = x.Phong.TongSoHang,
                        GhiChu = x.Phong.GhiChu
                    },
                    LoaiGhe = new GheModel.LoaiGheInfo
                    {
                        Id = x.LoaiGhe.Id,
                        GiaVe = x.LoaiGhe.GiaVe,
                        TenLoaiGhe = x.LoaiGhe.TenLoaiGhe
                    }
                }).ToList();
            return dsGhe;
        }
        [HttpPost("PhatSinhGheTheoPhong")]
        public ThongBaoModel PhatSinhGheTheoPhong(GheModel.Input.PhatSinhGheTheoPhong input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                var dsThongTin = _iXuLyThongTinGhe.DocDanhSachThongTinGheTheoPhong(input.PhongId);
                if (dsThongTin == null || dsThongTin.Count == 0)
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Chưa có thông tin của các dãy ghế trong phòng.";
                }
                else
                {
                    var result = _iXuLyGhe.PhatSinhGheTheoPhong(dsThongTin);
                    if (!result)
                    {
                        tb.MaSo = 3;
                        tb.NoiDung = "Không phát sinh được danh sách ghế";
                    }
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Không phát sinh được danh sách ghế. " + ex.Message;
            }            
            return tb;
        }
    }
}
