using Application.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T>, IReadRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public MyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<T>().Where(predicate);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task DesasociarAsync(T entidad)
        {
            _dbContext.Entry(entidad).State = EntityState.Detached;
            await Task.CompletedTask;
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        // Propiedad para acceder al DbContext y usar (por ejemplo) las extensiones de repositorio
        public DbContext DbContext => _dbContext;
    }
}
