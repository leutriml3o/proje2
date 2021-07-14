using Microsoft.EntityFrameworkCore;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Repository.Concrete
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SportDatabaseContext _context;
        public EfGenericRepository(SportDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await Save();
        }

        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await Save();
        }

        public async Task<int> Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await Save();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
   
        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            // return context.Set<TEntity>().SingleOrDefault(filter);
            return await _context.Set<T>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        //Solda kosul, sagda nesne var.
        public async Task<IQueryable<T>> Include(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IEnumerable<T> enumarableList = await GetAll();
            IQueryable<T> query = enumarableList.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<int> Save()
        {
           int success = await _context.SaveChangesAsync();
            return success;
        }

        public async Task<T> AddEntityAndGetId(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
