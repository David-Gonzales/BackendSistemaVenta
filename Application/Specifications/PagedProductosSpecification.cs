using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public  class PagedProductosSpecification : Specification<Producto>
    {
        public PagedProductosSpecification(int pageSize, int pageNumber, string? parametros)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(parametros))
            {
                Query.Where(x =>
                    EF.Functions.Like(x.Nombre, $"%{parametros}") ||
                    EF.Functions.Like(x.Unidad, $"%{parametros}%")
                );
            }

            // Cargar la relación de EstadosProducto
            Query.Include(x => x.Estados);
        }
    }
}
