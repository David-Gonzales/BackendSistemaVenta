using Ardalis.Specification;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task DesasociarAsync(T entity);
    }
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class{ }

}
