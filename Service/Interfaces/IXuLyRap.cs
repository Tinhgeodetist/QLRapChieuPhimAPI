using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLyRap : IBaseRepository<Rap>
    {
        List<Rap> DocDanhSachRap();

        Rap DocThongTinRap(int id);

    }
}
