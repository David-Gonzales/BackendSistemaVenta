using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class TransaccionesSpecification : Specification<Transaccion>
    {
        public TransaccionesSpecification(string tipoTransaccion)
        {
            Query.Include(p => p.Producto)
                .Include(u => u.Usuario);

            if (!string.IsNullOrEmpty(tipoTransaccion))
            {
                // Intentamos convertir el string a un valor del enum
                if (Enum.TryParse<TipoTransaccion>(tipoTransaccion, ignoreCase: true, out var tipoTransaccionEnum))
                {
                    // Comparamos con el valor subyacente del enum (sin ToString)
                    Query.Where(x => x.TipoTransaccion == tipoTransaccionEnum);
                }
                else
                {
                    // Si no se puede convertir, podemos filtrar por un valor predeterminado o manejarlo de otro modo
                    // Por ejemplo:
                    //Query.Where(x => x.TipoTransaccion == TipoTransaccion.Default);  // Suponiendo que haya un valor Default
                }
            }
        }
    }
}
