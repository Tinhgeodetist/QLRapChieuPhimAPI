using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class VeController : ControllerBase
    {
        private readonly IXuLyVe _iXuLyVe;
        public VeController(IXuLyVe iXuLyVe)
        {
            _iXuLyVe = iXuLyVe;
        }

        [HttpPost("MuaVe")]
        public ThongBaoModel MuaVe(VeModel.Input.MuaVe input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                if(input == null || input.DanhSachVe.Count == 0)
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Thông tin vé mua không hợp lệ.";
                }
                else
                {
                    var dsVe = new List<Ve>();
                    foreach(var ve in input.DanhSachVe)
                    {
                        var vemua = new Ve
                        {
                            NgayBanVe = DateTime.Today.Date,
                            GheId = ve.GheId,
                            GiaVe = ve.GiaVe,
                            LichChieuId = ve.LichChieuId,
                            NhanVienId = ve.NhanVienId,
                            SoGhe = ve.SoGhe,
                            TinhTrang = 2
                        };
                        dsVe.Add(vemua);
                    }
                    if (!_iXuLyVe.MuaVe(dsVe))
                    {
                        tb.MaSo = 3;
                        tb.NoiDung = "Có lỗi trong quá trình mua vé. Vui lòng thử lại.";
                    }
                }
            }
            catch (Exception ex)
            {
                tb.MaSo = 2;
                tb.NoiDung = "Lỗi mua vé. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("DanhSachVeTheoLichChieu")]
        public  List<VeModel.Output.ThongTinVe> DanhSachVeTheoLichChieu(VeModel.Input.DocDanhSachVeTheoLichChieu input)
        {
            var dsVe = _iXuLyVe.DocDanhSachVeTheoLichChieu(input.LichChieuId)
                .Select(x => new VeModel.Output.ThongTinVe { 
                    Id = x.Id,
                    GheId = x.GheId,
                    GiaVe = x.GiaVe,
                    LichChieuId = x.LichChieuId,
                    NhanVienId = x.NhanVienId,
                    SoGhe = x.SoGhe,
                    TinhTrang = x.TinhTrang
                }).ToList();
            return dsVe;
        }
        [HttpPost("DanhSachVeKhongDuocMuaTheoLichChieu")]
        public List<VeModel.Output.ThongTinVe> DanhSachVeKhongDuocMuaTheoLichChieu(VeModel.Input.DocDanhSachVeTheoLichChieu input)
        {
            var dsVe = _iXuLyVe.DocDanhSachVeKhongDuocMuaTheoLichChieu(input.LichChieuId)
                .Select(x => new VeModel.Output.ThongTinVe
                {
                    Id = x.Id,
                    GheId = x.GheId,
                    GiaVe = x.GiaVe,
                    LichChieuId = x.LichChieuId,
                    NhanVienId = x.NhanVienId,
                    SoGhe = x.SoGhe,
                    TinhTrang = x.TinhTrang
                }).ToList();
            return dsVe;
        }
    }
}
