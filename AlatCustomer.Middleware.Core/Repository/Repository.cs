using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected internal readonly DbContext _context;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
