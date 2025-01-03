using Ardalis.Specification;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken);
    }
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class{ }

}
