using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;

namespace Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<T> FirstOrDefaultAsync<T>(this IRepositoryAsync<T> repositoryAsync, CancellationToken cancellationToken = default) where T : class
        {
            var dbContext = (repositoryAsync as MyRepositoryAsync<T>)?.DbContext;
            if (dbContext == null)
            {
                throw new InvalidOperationException("El repositorio no tiene un DbContext asociado.");
            }

            return await dbContext.Set<T>().FirstOrDefaultAsync(cancellationToken);
        }
    }
}
