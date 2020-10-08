

namespace ContactManagement.Data.Abstraction
{
    using ContactManagement.Data.Models.BaseClass;

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll(CancellationToken token);
        Task<T> Get(long id, CancellationToken token);
        Task Insert(T entity, CancellationToken token);
        Task Update(T entity, CancellationToken token);
        Task Delete(T entity, CancellationToken token);

        Task<List<T>> FindByExpression(Expression<Func<T, bool>> predicate, CancellationToken token);

        Task<List<T>> FindByExpression(Expression<Func<T, bool>> predicate, CancellationToken token, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken token);

        Task<T> FirstOrDefault(CancellationToken token, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindByExpression(CancellationToken token, params Expression<Func<T, object>>[] includeProperties);
    }
}
