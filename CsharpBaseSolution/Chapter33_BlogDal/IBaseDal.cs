using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_BlogDal
{
    public interface IBaseDal<T>where T:class,new ()
    {
        T GetEntity(Expression<Func<T, bool>> whereLambda);
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        void InsertEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        long Count();
    }
}
