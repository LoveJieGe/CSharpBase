using Chapter33_BlogLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogDal
{
    public class BaseDal<T>where T:class,new()
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseDal(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }
        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return _dbSet.FirstOrDefault(whereLambda);
        }
        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            if(whereLambda==null)
            {
                return _dbSet.AsEnumerable();
            }
            return _dbSet.Where(whereLambda);
        }
        public void InsertEntity(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void UpdateEntity(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void DeleteEntity(T entity)
        {
            _dbSet.Remove(entity);
        }
        public long Count()
        {
            return _dbSet.LongCount();
        }
        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
