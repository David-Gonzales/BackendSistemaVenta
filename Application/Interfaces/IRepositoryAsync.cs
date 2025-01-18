using Ardalis.Specification;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class{ }

}
