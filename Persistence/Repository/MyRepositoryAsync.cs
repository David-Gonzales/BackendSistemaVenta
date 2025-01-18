using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public MyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "El predicado no puede ser nulo.");
            }

            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        // Propiedad para acceder al DbContext y usar (por ejemplo) las extensiones de repositorio
        public DbContext DbContext => _dbContext;
    }
}
