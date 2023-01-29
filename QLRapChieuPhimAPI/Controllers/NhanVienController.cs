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
    public class NhanVienController : ControllerBase
    {
        private readonly IXuLyNhanVien _iXuLyNhanVien;
        private readonly IJwtAuthManager _jwtAuthManager;
        public NhanVienController(IXuLyNhanVien iXuLyNhanVien, IJwtAuthManager jwtAuthManager)
        {
            _iXuLyNhanVien = iXuLyNhanVien;
            _jwtAuthManager = jwtAuthManager;
        }
        [HttpPost("DangNhap")]
        public NhanVienModel.Output.ThongTinNhanVien DangNhap(NhanVienModel.Input.ThongTinDangNhap input)
        {
            NhanVienModel.Output.ThongTinNhanVien thongTinNhanVien = new();
            var nhanvien = _iXuLyNhanVien.DangNhap(input.TenDangNhap, input.MatKhau);
            if (nhanvien != null)
            {
                Utilities.PropertyCopier<NhanVien, NhanVienModel.Output.ThongTinNhanVien>.Copy(nhanvien, thongTinNhanVien);
                var userInfo = new UserInfo
                {
                    Id = nhanvien.Id,
                    Email = "",
                    HoTen = nhanvien.HoTen,
                    Username = nhanvien.Cmnd,
                    UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                };
                thongTinNhanVien.AccessToken = _jwtAuthManager.CreateToken(userInfo);
            }
            return thongTinNhanVien;
        }
        [HttpPost("DanhSachNhanVien")]
        public List<NhanVienModel.Output.ThongTinNhanVien> DanhSachNhanVien(NhanVienModel.Input.DanhSachNhanVien input)
        {
            var dsnhanvien = new List<NhanVienModel.Output.ThongTinNhanVien>();
            if (input.QuanTri)
                dsnhanvien = _iXuLyNhanVien.DocDanhSach()
                                              .Select(x => new NhanVienModel.Output.ThongTinNhanVien
                                              {
                                                  Id = x.Id,
                                                  HoTen = x.HoTen,
                                                  DiaChi = x.DiaChi,
                                                  Cmnd = x.Cmnd,
                                                  GioiTinh = x.GioiTinh,
                                                  NgaySinh = x.NgaySinh,
                                                  QuyenHan = x.QuyenHan,
                                                  RapId = x.RapId
                                              }).ToList();
            return dsnhanvien;
        }
        [HttpPost("ThayDoiMatKhau")]
        public ThongBaoModel ThayDoiMatKhau(NhanVienModel.Input.ThongTinThayDoiMatKhau nhan_vien)
        {
            ThongBaoModel tb = new ThongBaoModel();
            var tb_ = _iXuLyNhanVien.ThayDoiMatKhau(nhan_vien.Id, nhan_vien.Username, nhan_vien.MatKhauCu, nhan_vien.MatKhauMoi);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;
            return tb;
        }

        [Authorize]
        [HttpPost("DangXuat")]
        public bool Logout(NhanVienModel.Input.ThongTinDangNhap input)
        {
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
    }
}
