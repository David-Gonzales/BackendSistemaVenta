using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class VentasSpecification : Specification<Venta>
    {
        public VentasSpecification() {

            Query.Include(dv => dv.DetalleVentas)
                    .Include(c => c.Cliente);

        }
    }
}
