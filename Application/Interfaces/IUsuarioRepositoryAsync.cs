using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioRepositoryAsync : IRepositoryAsync<Usuario>
    {
        Task<Usuario> GetByCorreoAsync(string correo);
    }
}
