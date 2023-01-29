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
    [APIAuthorization]
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IXuLyPhim _iXuLyPhim;
        private readonly IXuLyTheLoaiPhim _iXuLyTheLoaiPhim;
        private readonly IXuLyXepHangPhim _iXuLyXepHangPhim;
        public PhimController(IXuLyPhim iXuLyPhim, IXuLyTheLoaiPhim iXuLyTheLoaiPhim, 
                                                   IXuLyXepHangPhim iXuLyXepHangPhim) {
            _iXuLyPhim = iXuLyPhim;
            _iXuLyTheLoaiPhim = iXuLyTheLoaiPhim;
            _iXuLyXepHangPhim = iXuLyXepHangPhim;
        }

        [HttpPost("DanhSachPhimDangChieu")]
        public List<PhimModel.Output.ThongTinPhim> DanhSachPhimDangChieu()
        {
            var phims = _iXuLyPhim.DocDanhSachPhimDangChieu();
            var dsPhim = phims.Select(p => new PhimModel.Output.ThongTinPhim
            {
                Id = p.Id,
                TenPhim = p.TenPhim,
                TenGoc = p.TenGoc,
                DaoDien = p.DaoDien,
                DienVien = p.DienVien,
                NgayKhoiChieu = p.NgayKhoiChieu == null ? DateTime.MinValue : p.NgayKhoiChieu.Value,
                DanhSachTheLoaiId = p.DanhSachTheLoaiId,
                NgonNgu = p.NgonNgu,
                NhaSanXuat = p.NhaSanXuat,
                NoiDung = p.NoiDung,
                NuocSanXuat = p.NuocSanXuat,
                Poster = p.Poster,
                ThoiLuong = p.ThoiLuong,
                Trailer = p.Trailer,
                XepHangPhimId = p.XepHangPhimId,
                XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                {
                    Id = p.XepHangPhim.Id,
                    Ten = p.XepHangPhim.Ten,
                    KyHieu = p.XepHangPhim.KyHieu
                }
            }).ToList();
            var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
            foreach (var phim in dsPhim) {
                var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim.Count > 0)
                    foreach (var tl in theloai_phim) {
                        phim.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                        {
                            Id = tl.Id,
                            Ten = tl.Ten
                        });
                    }
            }
            return dsPhim;
        }

        [HttpPost("DanhSachPhimSapChieu")]
        public List<PhimModel.Output.ThongTinPhim> DanhSachPhimSapChieu()
        {
            var phims = _iXuLyPhim.DocDanhSachPhimSapChieu();
            var dsPhim = phims.Select(p => new PhimModel.Output.ThongTinPhim {
                Id = p.Id,
                TenPhim = p.TenPhim,
                TenGoc = p.TenGoc,
                DaoDien = p.DaoDien,
                DienVien = p.DienVien,
                NgayKhoiChieu = p.NgayKhoiChieu == null ? DateTime.MinValue : p.NgayKhoiChieu.Value,
                DanhSachTheLoaiId = p.DanhSachTheLoaiId,
                NgonNgu = p.NgonNgu,
                NhaSanXuat = p.NhaSanXuat,
                NoiDung = p.NoiDung,
                NuocSanXuat = p.NuocSanXuat,
                Poster = p.Poster,
                ThoiLuong = p.ThoiLuong,
                Trailer = p.Trailer,
                XepHangPhimId = p.XepHangPhimId,
                XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                {
                    Id = p.XepHangPhim.Id,
                    Ten = p.XepHangPhim.Ten,
                    KyHieu = p.XepHangPhim.KyHieu
                }
            }).ToList();
            var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
            foreach (var phim in dsPhim) {
                var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim.Count > 0)
                    foreach (var tl in theloai_phim) {
                        phim.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase {
                            Id = tl.Id, Ten = tl.Ten
                        });
                    }
            }
            return dsPhim;
        }
        [HttpPost("DanhSachPhimTheoTheLoai")]
        public PhimModel.Output.PhimTheoTheLoai DanhSachPhimTheoTheLoai(PhimModel.Input.PhimTheoTheLoai input)
        {
            var result = new PhimModel.Output.PhimTheoTheLoai();
            
            

            var dsPhimTheoTheLoai = _iXuLyPhim.DocDanhSachPhimTheoTheLoai(input.TheLoaiId, input.CurrentPage, input.PageSize);
            if (input.PageSize <= 0)
                input.PageSize = 20;
            float numberpage = (float)dsPhimTheoTheLoai.Count() / input.PageSize;
            int pageCount = (int)Math.Ceiling(numberpage);

            int currentPage = input.CurrentPage;
            if (currentPage > pageCount) currentPage = pageCount;

            var dsPhim = dsPhimTheoTheLoai.Select(p => new PhimModel.Output.ThongTinPhim
            {
                Id = p.Id,
                TenPhim = p.TenPhim,
                TenGoc = p.TenGoc,
                DaoDien = p.DaoDien,
                DienVien = p.DienVien,
                NgayKhoiChieu = p.NgayKhoiChieu == null ? DateTime.MinValue : p.NgayKhoiChieu.Value,
                DanhSachTheLoaiId = p.DanhSachTheLoaiId,
                NgonNgu = p.NgonNgu,
                NhaSanXuat = p.NhaSanXuat,
                NoiDung = p.NoiDung,
                NuocSanXuat = p.NuocSanXuat,
                Poster = p.Poster,
                ThoiLuong = p.ThoiLuong,
                Trailer = p.Trailer,
                XepHangPhimId = p.XepHangPhimId,
                XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                {
                    Id = p.XepHangPhim.Id,
                    Ten = p.XepHangPhim.Ten,
                    KyHieu = p.XepHangPhim.KyHieu
                }
            }).ToList();
            var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
            foreach (var phim in dsPhim)
            {
                var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim.Count > 0)
                    foreach (var tl in theloai_phim)
                    {
                        phim.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                        {
                            Id = tl.Id,
                            Ten = tl.Ten
                        });
                    }
            }
            if (input.TheLoaiId != 0)
            {
                var tl = dsTheLoai.FirstOrDefault(x => x.Id.Equals(input.TheLoaiId));
                result.TheLoaiHienHanh = new TheLoaiPhimModel.TheLoaiPhimBase
                {
                    Id = input.TheLoaiId,
                    Ten = tl.Ten
                };
            }
            result.DanhSachPhim = dsPhim;
            result.CurrentPage = input.CurrentPage;
            result.PageCount = pageCount;
            result.DanhSachTheLoai = dsTheLoai.Select(t => new TheLoaiPhimModel.TheLoaiPhimBase { Id = t.Id, Ten = t.Ten }).ToList();
            return result;
        }

        [HttpPost("ThongTinPhim")]
        public PhimModel.Output.ThongTinPhim ThongTinPhim(PhimModel.Input.DocThongTinPhim input)
        {
            PhimModel.Output.ThongTinPhim thongTinPhim = new PhimModel.Output.ThongTinPhim();
            var phim = _iXuLyPhim.DocThongTinPhim(input.PhimId);
            if (phim != null && phim.Id > 0)
            {
                thongTinPhim = new();
                Utilities.PropertyCopier<Phim, PhimModel.Output.ThongTinPhim>.Copy(phim, thongTinPhim);
                thongTinPhim.Id = phim.Id;
                thongTinPhim.XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                {
                    Id = phim.XepHangPhim.Id,
                    KyHieu = phim.XepHangPhim.KyHieu,
                    Ten = phim.XepHangPhim.Ten
                };
                var dsTheLoai = _iXuLyTheLoaiPhim.DocDanhSachTheLoai();
                //var dsXepHangPhim = _iXuLyXepHangPhim.DocDanhSachXepHangPhim();
                var dsIdTheLoai = thongTinPhim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim.Count > 0)
                    foreach (var tl in theloai_phim)
                    {
                        thongTinPhim.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                        {
                            Id = tl.Id,
                            Ten = tl.Ten
                        });
                    }
                
            }
            return thongTinPhim;
        }

        [HttpPost("ThemPhimMoi")]
        public bool ThemPhimMoi(PhimModel.Input.ThongTinPhim phim)
        {
            try
            {
                Phim phimThemMoi = new();
                Utilities.PropertyCopier<PhimModel.PhimBase, Phim>.Copy(phim, phimThemMoi);
                _iXuLyPhim.Them(phimThemMoi);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("CapNhatPhim")]
        public bool CapNhatPhim(PhimModel.Input.ThongTinPhim phim)
        {
            try
            {
                var phimCapNhat = _iXuLyPhim.DocThongTinPhim(phim.Id);
                if(phimCapNhat != null)
                {
                    Utilities.PropertyCopier<PhimModel.PhimBase, Phim>.Copy(phim, phimCapNhat);
                    return _iXuLyPhim.Sua(phimCapNhat);
                }
                return false;                
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("XoaPhim")]
        public bool XoaPhim(PhimModel.Input.XoaPhim input)
        {
            try
            {
                var phim = _iXuLyPhim.DocThongTinPhim(input.PhimId);
                if (phim == null) return false;
                var kq = _iXuLyPhim.Xoa(phim);
                return kq;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
