using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class DetalleVentaSpecification : Specification<DetalleVenta>
    {
        public DetalleVentaSpecification() {

            Query.Include(dv => dv.Producto);
        }
    }
}
