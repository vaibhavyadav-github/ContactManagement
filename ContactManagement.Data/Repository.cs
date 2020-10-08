

namespace ContactManagement.Data
{
    using ContactManagement.Data.Abstraction;
    using ContactManagement.Data.Models.BaseClass;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ContactDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(ContactDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll(CancellationToken token)
        {
            return await entities.ToListAsync(token).ConfigureAwait(false);
        }
        public async Task<T> Get(long id, CancellationToken token)
        {
            return await entities.SingleOrDefaultAsync(s => s.UniqueId == id,
                                                       token).ConfigureAwait(false);
        }
        public async Task Insert(T entity, CancellationToken token)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        public async Task Update(T entity, CancellationToken token)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        public async Task Delete(T entity, CancellationToken token)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<List<T>> FindByExpression(Expression<Func<T,bool>> predicate, CancellationToken token)
        {
            return await entities.Where(predicate).ToListAsync(token).ConfigureAwait(false);
        }

        public async Task<List<T>> FindByExpression(Expression<Func<T, bool>> predicate, CancellationToken token, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.Where(predicate).AsQueryable();

            includeProperties.ToList().ForEach(
                navitable => {
                    if (navitable != null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });

            return await query.ToListAsync(token).ConfigureAwait(false);

        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            return await entities.FirstOrDefaultAsync(predicate,token).ConfigureAwait(false);
        }
        public async Task<T> FirstOrDefault(CancellationToken token, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.AsQueryable();

            includeProperties.ToList().ForEach(
                navitable => { 
                if(navitable!= null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });                

            return await query.FirstOrDefaultAsync(predicate, token).ConfigureAwait(false);
        }

        public async Task<List<T>> FindByExpression(CancellationToken token, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = entities.AsQueryable();

            includeProperties.ToList().ForEach(
                navitable => {
                    if (navitable != null)
                    {
                        query = includeProperties.Aggregate(query,
                                                            (current, expression) => current.Include(navitable));

                    }
                });

            return await query.ToListAsync(token).ConfigureAwait(false);
        }
    }
}
