using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class PagedVentasSpecification : Specification<Venta>
    {
        public PagedVentasSpecification(int pageSize, int pageNumber, string numeroVenta, DateTime fechaInicio, DateTime fechaFin)
        {
            Query.Include(dv => dv.DetalleVentas)
                .Include(c => c.Cliente)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(numeroVenta))
                Query.Search(x => x.NumeroVenta, "%" + numeroVenta + "%");

            if (fechaInicio > fechaFin)
            {
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            if (fechaInicio != DateTime.MinValue && fechaFin != DateTime.MinValue)
            {
                Query.Where(x => x.Created >= fechaInicio && x.Created <= fechaFin);
            }
        }
    }
}
