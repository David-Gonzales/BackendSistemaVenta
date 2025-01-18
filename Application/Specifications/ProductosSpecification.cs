using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ProductosSpecification : Specification<Producto>
    {
        public ProductosSpecification() 
        {
            Query.Include(p => p.Estados)
                .AsNoTracking();
        }
        
    }
}
