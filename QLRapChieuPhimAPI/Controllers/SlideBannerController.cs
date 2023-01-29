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
    public class SlideBannerController : ControllerBase
    {
        private readonly IXuLySlideBanner _iXuLySlideBanner;
        public SlideBannerController(IXuLySlideBanner iXuLySlideBanner)
        {
            _iXuLySlideBanner = iXuLySlideBanner;
        }
        [HttpPost("DanhSachSlideBanner")]
        public List<SlideBannerModel.Output.ThongTinSlideBanner> DanhSachSlideBanner(SlideBannerModel.Input.DocDanhSachSlideBanner input)
        {
            var slides = _iXuLySlideBanner.DocDanhSachSlideBanner(input.QuanTri);
            var dsSlide = slides.Select(s => new SlideBannerModel.Output.ThongTinSlideBanner { 
                Id = s.Id,
                Ten = s.Ten,
                KichHoat = s.KichHoat,
                LienKet = s.LienKet,
                Hinh = s.Hinh
            }).ToList();
            return dsSlide;
        }
        [HttpPost("ThemSlideBanner")]
        public ThongBaoModel ThemSlideBannerMoi(SlideBannerModel.Output.ThongTinSlideBanner slideBanner)
        {
            var tb = new ThongBaoModel { MaSo = 1, NoiDung = "" };
            try
            {
                var banner = _iXuLySlideBanner.Them(new Service.Models.SlideBanner
                {
                    Id = slideBanner.Id,
                    Ten = slideBanner.Ten,
                    Hinh = slideBanner.Hinh,
                    LienKet = slideBanner.LienKet,
                    KichHoat = slideBanner.KichHoat
                });
                if (banner != null)
                    tb.NoiDung = banner.Id.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không thêm được Slide Banner";
                }
            }
            catch (Exception)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không thêm được Slide Banner";
            }
            return tb;
        }
        [HttpPost("ThongTinSlideBanner")]
        public SlideBannerModel.Output.ThongTinSlideBanner XemThongTinSlideBanner(SlideBannerModel.Input.DocThongTinSlideBanner input)
        {
            var slideBanner = _iXuLySlideBanner.DocThongTinSlideBanner(input.Id);
            SlideBannerModel.Output.ThongTinSlideBanner thongTinSlideBanner = new SlideBannerModel.Output.ThongTinSlideBanner
            {
                Id = slideBanner.Id,
                Ten = slideBanner.Ten,
                Hinh = slideBanner.Hinh,
                LienKet = slideBanner.LienKet,
                KichHoat = slideBanner.KichHoat
            };
            return thongTinSlideBanner;
        }
        [HttpPost("CapNhatSlideBanner")]
        public ThongBaoModel CapNhatSlideBanner(SlideBannerModel.Input.ThongTinSlideBanner thongTinSlideBanner)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                Service.Models.SlideBanner slideBanner = new()
                {
                    Id = thongTinSlideBanner.Id,
                    Ten = thongTinSlideBanner.Ten,
                    Hinh = thongTinSlideBanner.Hinh,
                    LienKet = thongTinSlideBanner.LienKet,
                    KichHoat = thongTinSlideBanner.KichHoat
                };
                if(!_iXuLySlideBanner.Sua(slideBanner))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật được SlideBanner";
                }
            }
            catch (Exception)
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không cập nhật được SlideBanner";
            }
            return tb;
        }
        [HttpPost("XoaSlideBanner")]
        public ThongBaoModel XoaSlideBanner(SlideBannerModel.Input.XoaSlideBanner input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            if(input.Id > 0)
            {
                var banner = _iXuLySlideBanner.DocThongTinSlideBanner(input.Id);
                if (banner != null && _iXuLySlideBanner.Xoa(banner))
                    tb.NoiDung = banner.Hinh.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được Slide Banner";
                }
            }
            else
            {
                tb.MaSo = 1;
                tb.NoiDung = "Thông tin SlideBanner không hợp lệ";
            }
            return tb;
        }
    }
}
