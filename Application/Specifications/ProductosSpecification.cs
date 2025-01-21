using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class ProductosSpecification : Specification<Producto>
    {
        public ProductosSpecification(string? parametros) 
        {
            Query.Include(p => p.Estados)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(parametros))
            {
                Query.Where(x =>
                    EF.Functions.Like(x.Nombre, $"%{parametros}") ||
                    EF.Functions.Like(x.Unidad, $"%{parametros}%")
                );
            }
        }
        
    }
}
