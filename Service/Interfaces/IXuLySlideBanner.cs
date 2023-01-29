using Service.Base;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IXuLySlideBanner : IBaseRepository<SlideBanner>
    {
        List<SlideBanner> DocDanhSachSlideBanner(bool quantri = false);

        SlideBanner DocThongTinSlideBanner(int id);

    }
}
