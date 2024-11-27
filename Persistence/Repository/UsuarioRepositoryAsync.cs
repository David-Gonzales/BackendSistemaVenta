using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public  class UsuarioRepositoryAsync : MyRepositoryAsync<Usuario>, IUsuarioRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetByCorreoAsync(string correo)
        {
            return await _dbContext.Set<Usuario>()
                .Include(u => u.Rol) // Necesito incluir el Rol.
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
