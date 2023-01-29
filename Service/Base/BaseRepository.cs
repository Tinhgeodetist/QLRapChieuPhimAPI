using Microsoft.EntityFrameworkCore;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly QLRapChieuPhimContext _context;
        public BaseRepository(QLRapChieuPhimContext context)
        {
            this._context = context;
        }
        public T Them(T entity)
        {            
            try
            {
                var ent = _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return ent.Entity;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool Xoa(T entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<T> DocTheoDieuKien(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(where);
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public IEnumerable<T> DocDanhSach(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (Expression<Func<T, object>> i in includes)
            {
                query = query.Include(i);
            }
            return query.ToList();
        }

        public bool Sua(T entity)
        {            
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
