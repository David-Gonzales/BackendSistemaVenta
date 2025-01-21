using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class UsuariosSpecification : Specification<Usuario>
    {
        public UsuariosSpecification(string? parametros) 
        {
            Query.Include(r => r.Rol);

            if (!string.IsNullOrEmpty(parametros))
            {
                Query.Where(x =>
                    EF.Functions.Like(x.Nombres, $"%{parametros}") ||
                    EF.Functions.Like(x.Apellidos, $"%{parametros}%") ||
                    EF.Functions.Like(x.Correo, $"%{parametros}%")
                );
            }
        }
    }
}
