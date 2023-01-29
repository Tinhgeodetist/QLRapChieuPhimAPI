using Service.Base;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class XuLyNhanVien : BaseRepository<NhanVien>, IXuLyNhanVien
    {
        public XuLyNhanVien(QLRapChieuPhimContext context) : base(context) { }

        public NhanVien DangNhap(string TenDangNhap, string MatKhau)
        {
            var NhanVien = new NhanVien();
            if (!string.IsNullOrEmpty(TenDangNhap) && !string.IsNullOrEmpty(MatKhau))
            {
                try
                {
                    NhanVien = _context.NhanViens.FirstOrDefault(x => x.Cmnd.Equals(TenDangNhap)
                                        && x.MatKhau.Equals(MatKhau));
                }
                catch { }
            }
            return NhanVien;
        }

        public ThongBao ThayDoiMatKhau(int Id, string Username, string MatKhauCu, string MatKhauMoi)
        {
            ThongBao tb = new ThongBao { MaSo = 1 };
            try
            {
                var nhanVienCapNhat = _context.NhanViens.FirstOrDefault(p => p.Id.Equals(Id) &&
                                                                                    p.Cmnd.Equals(Username));
                if (nhanVienCapNhat != null)
                {
                    if (nhanVienCapNhat.MatKhau == MatKhauCu)
                    {
                        nhanVienCapNhat.MatKhau = MatKhauMoi;
                        int kq = _context.SaveChanges();
                        if (kq > 0)
                        {
                            tb.MaSo = 0;
                            tb.NoiDung = "Thay đổi mật khẩu thành công";
                        }
                        else
                            tb.NoiDung = "Thay đổi mật khẩu không thành công";
                    }
                    else
                        tb.NoiDung = "Mật khẩu cũ không đúng.";
                }
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }
    }
}
