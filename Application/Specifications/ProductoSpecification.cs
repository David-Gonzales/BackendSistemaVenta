using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ProductoSpecification : Specification<Producto>
    {
        public ProductoSpecification(int id, bool asNoTracking = false) 
        {
            Query.Where(p => p.Id == id)
             .Include(p => p.Estados); // Incluye la relación Estados

            if (asNoTracking)
            {
                Query.AsNoTracking(); // Esto aplica al query dentro de la especificación
            }
        }
    }
}
