using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhimAPI.Auth;
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
    public class ThanhVienController : ControllerBase
    {
        private readonly IXuLyThanhVien _iXuLyThanhVien;
        private readonly IJwtAuthManager _jwtAuthManager;
        public ThanhVienController(IXuLyThanhVien iXuLyThanhVien, IJwtAuthManager jwtAuthManager)
        {
            _iXuLyThanhVien = iXuLyThanhVien;
            _jwtAuthManager = jwtAuthManager;
        }
        [HttpPost("DangNhapAnDanh")]
        public ThanhVienModel.Output.ThongTinThanhVien DangNhapAnDanh()
        {
            var userInfo = new UserInfo
            {
                Id = 0,
                Email = "",
                HoTen = "",
                Username = Guid.NewGuid().ToString(),
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            };
            ThanhVienModel.Output.ThongTinThanhVien thongTinThanhVien = new()
            {
                Id = 0,
                Email = "",
                DienThoai = "",
                GioiTinh = false,
                HoTen = "Anonymous",
                KichHoat = true,
                MatKhau = "",
                NgaySinh = DateTime.Today.Date,
                SocialLogin = "",
                AccessToken = _jwtAuthManager.CreateToken(userInfo)
            };
            return thongTinThanhVien;
        }

        [HttpPost("DangNhap")]
        public ThanhVienModel.Output.ThongTinThanhVien DangNhap(ThanhVienModel.Input.ThongTinDangNhap input)
        {
            ThanhVienModel.Output.ThongTinThanhVien thongTinThanhVien = new();
            var thanh_vien = _iXuLyThanhVien.DangNhap(input.TenDangNhap, input.MatKhau);
            if (thanh_vien != null && thanh_vien.Id > 0)
            {
                Utilities.PropertyCopier<ThanhVien, ThanhVienModel.Output.ThongTinThanhVien>.Copy(thanh_vien, thongTinThanhVien);
                var userInfo = new UserInfo
                {
                    Id = thanh_vien.Id,
                    Email = thanh_vien.Email,
                    HoTen = thanh_vien.HoTen,
                    Username = thanh_vien.Email,
                    UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                };
                thongTinThanhVien.AccessToken = _jwtAuthManager.CreateToken(userInfo);
            }
            
            return thongTinThanhVien;
        }

        [Authorize]
        [HttpPost("DangXuat")]
        public bool Logout(ThanhVienModel.Input.ThongTinDangNhap input)
        {
            //string rawUserId = HttpContext.User.Identity.Name;
            try
            {
                _jwtAuthManager.RemoveTokenByUserName(input.TenDangNhap);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        [HttpPost("DangKyThanhVien")]
        public ThongBaoModel DangKyThanhVien(ThanhVienModel.Input.ThongTinThanhVienMoi thanh_vien)
        {
            ThongBaoModel tb = new ThongBaoModel { MaSo = 1 };
            try
            {
                var chuoiTB = "";
                if (string.IsNullOrEmpty(thanh_vien.Email))
                    chuoiTB = "Email phải khác rỗng";
                else
                {
                    var thanhvien = _iXuLyThanhVien.TimThanhVien(thanh_vien.Email);
                    if (thanhvien != null)
                        chuoiTB = "Email này đã được sử dụng rồi.";
                }
                if (string.IsNullOrEmpty(thanh_vien.HoTen))
                    chuoiTB = "Họ tên phải khác rỗng";
                if (string.IsNullOrEmpty(thanh_vien.DienThoai))
                    chuoiTB = "Điện thoại phải khác rỗng";
                if (string.IsNullOrEmpty(chuoiTB))
                {
                    var thanh_vien_moi = new ThanhVien();
                    Utilities.PropertyCopier<ThanhVienModel.Input.ThongTinThanhVienMoi, ThanhVien>.Copy(thanh_vien, thanh_vien_moi);
                    var tb_ = _iXuLyThanhVien.DangKyThanhVien(thanh_vien_moi);
                    tb.MaSo = tb_.MaSo;
                    chuoiTB = tb_.NoiDung;
                }
                tb.NoiDung = chuoiTB;
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }
        [HttpPost("ThayDoiMatKhau")]
        public ThongBaoModel ThayDoiMatKhau(ThanhVienModel.Input.ThongTinThayDoiMatKhau thanh_vien)
        {
            ThongBaoModel tb = new ThongBaoModel();
            var tb_ = _iXuLyThanhVien.ThayDoiMatKhau(thanh_vien.Id, thanh_vien.Username, thanh_vien.MatKhauCu, thanh_vien.MatKhauMoi);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;
            return tb;
        }
        [HttpPost("ThongTinThanhVien")]
        public ThanhVienModel.Output.ThongTinThanhVien ThongTinThanhVien(ThanhVienModel.Input.ThongTinThanhVien input)
        {
            var thanh_vien = _iXuLyThanhVien.DocThongTinThanhVien(input.Id);
            var thanh_vien_ = new ThanhVienModel.Output.ThongTinThanhVien();
            Utilities.PropertyCopier<ThanhVien, ThanhVienModel.Output.ThongTinThanhVien>.Copy(thanh_vien, thanh_vien_);
            return thanh_vien_;
        }
        [HttpPost("ThemThanhVien")]
        public ThongBaoModel ThemThanhVien(ThanhVienModel.Input.ThongTinThanhVienMoi thanh_vien_moi)
        {
            ThongBaoModel tb = new ThongBaoModel();
            ThanhVien thanh_vien = new();
            Utilities.PropertyCopier<ThanhVienModel.Input.ThongTinThanhVienMoi, ThanhVien>.Copy(thanh_vien_moi, thanh_vien);
            var tb_ = _iXuLyThanhVien.ThemThanhVien(thanh_vien);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;

            return tb;
        }
        [HttpPost("KichHoatTaiKhoan")]
        public ThongBaoModel KichHoatTaiKhoan(ThanhVienModel.Input.KichHoatTaiKhoan input)
        {
            ThongBaoModel tb = new ThongBaoModel();
            var tb_ = _iXuLyThanhVien.KichHoatTaiKhoan(input.Email);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;

            return tb;
        }
    }
}
