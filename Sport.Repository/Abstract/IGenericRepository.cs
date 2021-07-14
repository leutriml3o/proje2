using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Repository.Abstract
{
    public interface IGenericRepository<T> where T:class
    {
        // Task<T> Get(int id);

        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<int> Add(T entity);
        Task<int> Edit(T entity);
        Task<int> Delete(T entity);
        Task<IQueryable<T>> Include(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<int> Save();
        Task<T> AddEntityAndGetId(T entity);


    }
}
