using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Entities;
using System.Linq.Expressions;

namespace FinanceTracker.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetBySpec(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllBySpec(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetQueryableBySpec(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<bool> Exists(Guid id);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Entities { get; }
        Task SaveChanges();
        Task<List<Transaction>> GetAsyncUserId(Guid userId);
    }
}
