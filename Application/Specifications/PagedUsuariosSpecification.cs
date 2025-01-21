using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class PagedUsuariosSpecification : Specification<Usuario>
    {
        public PagedUsuariosSpecification(int pageSize, int pageNumber, string? parametros)
        {
            Query.Include(r => r.Rol).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(parametros))
            {
                //Es como un LIKE, compara
                //Query.Search(x => x.Nombres, "%" + parametros + "%");

                //Otra forma para ampliar la búsqueda
                Query.Where(x =>
                    EF.Functions.Like(x.Nombres, $"%{parametros}") ||
                    EF.Functions.Like(x.Apellidos, $"%{parametros}%") ||
                    EF.Functions.Like(x.Correo, $"%{parametros}%")
                    //EF.Functions.Like(x.Rol.Nombre, $"%{parametros}%")
                );
            }
        }
    }
}
