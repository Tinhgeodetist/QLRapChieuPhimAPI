using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyNhanVien : IBaseRepository<NhanVien>
    {
        NhanVien DangNhap(string TenDangNhap, string MatKhau);
        ThongBao ThayDoiMatKhau(int Id, string Username, string MatKhauCu, string MatKhauMoi);
    }
}
