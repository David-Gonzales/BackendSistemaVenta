using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class TransaccionConProductoYEstadosSpecification : Specification<Transaccion>
    {
        public TransaccionConProductoYEstadosSpecification(int transaccionId)
        {
            Query
                .Where(t => t.Id == transaccionId)
                .Include(t => t.Producto)
                    .ThenInclude(p => p.Estados)
                .AsNoTracking(); // Mejora de rendimiento: evita el seguimiento de EF Core si solo se va a leer
        }

        // Sobrecarga para buscar por otros criterios además del Id (opcional)
        public TransaccionConProductoYEstadosSpecification(int? transaccionId = null, int? productoId = null)
        {
            Query
                .Include(t => t.Producto)
                    .ThenInclude(p => p.Estados)
                .AsNoTracking();

            if (transaccionId.HasValue)
            {
                Query.Where(t => t.Id == transaccionId.Value);
            }

            if (productoId.HasValue)
            {
                Query.Where(t => t.IdProducto == productoId.Value);
            }
        }
    }
}
