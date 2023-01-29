using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public interface IBaseRepository<T> where T : class
    {
        T Them(T entity);
        bool Sua(T entity);
        bool Xoa(T entity);
        IEnumerable<T> DocDanhSach(params Expression<Func<T, object>>[] includes );
        IEnumerable<T> DocTheoDieuKien(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    }
}
