using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class PagedTransaccionesSpecification : Specification<Transaccion>
    {
        public PagedTransaccionesSpecification(int pageSize, int pageNumber, string parametros)
        {
            Query.Include(p => p.Producto)
                .Include(u => u.Usuario)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(parametros))
            {
                Query.Where(x =>
                    EF.Functions.Like(x.Usuario.Nombres, $"%{parametros}") ||
                    EF.Functions.Like(x.Usuario.Apellidos, $"%{parametros}%") ||
                    EF.Functions.Like(x.Producto.Nombre, $"%{parametros}%") ||
                    EF.Functions.Like(x.Producto.Unidad, $"%{parametros}%") ||
                    EF.Functions.Like(x.TipoEstado.ToString(), $"%{parametros}%")
                );
            }
        }
    }
}
