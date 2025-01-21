using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class PagedVentasSpecification : Specification<Venta>
    {
        public PagedVentasSpecification(int pageSize, int pageNumber, string? buscarPor, string? numeroVenta, string? fechaInicio, string ?fechaFin)
        {
            Query.Include(dv => dv.DetalleVentas)
                .Include(c => c.Cliente)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(numeroVenta))
                Query.Search(x => x.NumeroVenta, "%" + numeroVenta + "%");

            //if (fechaInicio > fechaFin)
            //{
            //    throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");
            //}

            buscarPor = buscarPor?.ToLowerInvariant() ?? string.Empty;

            if (string.IsNullOrEmpty(buscarPor))
            {
                // No se aplica ningún filtro, se listan todos los registros.
                return;
            }

            if (buscarPor == "fecha")
            {
                //if (fechaInicio != DateTime.MinValue && fechaFin != DateTime.MinValue)
                //{
                //    Query.Where(x => x.Created >= fechaInicio && x.Created <= fechaFin);
                //}
                //else
                //{
                //    throw new ArgumentException("Debe especificar una fecha de inicio y fin válidas.");
                //}

                if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
                {
                    // Tratar los valores vacíos como si no hubieran sido proporcionados.
                    fechaInicio = null;
                    fechaFin = null;
                }
                else
                {
                    DateTime parsedInicio;
                    DateTime parsedFin;

                    if (DateTime.TryParse(fechaInicio, out parsedInicio) && DateTime.TryParse(fechaFin, out parsedFin))
                    {
                        
                        // Ajustar el rango para incluir todo el día final
                        parsedFin = parsedFin.AddDays(1);
                        // Usar las fechas convertidas
                        Query.Where(x => x.Created >= parsedInicio && x.Created <= parsedFin);
                    }
                    else
                    {
                        throw new ArgumentException("Debe especificar fechas válidas.");
                    }
                }
            }
            else if(buscarPor == "numero" && !string.IsNullOrEmpty(numeroVenta))
            {
                //Query.Where(x => x.NumeroVenta == numeroVenta);
                Query.Where(x => x.NumeroVenta.Contains(numeroVenta));
            }
            else
            {
                throw new ArgumentException("Parámetros incorrectos.");
            }
            
        }

        public PagedVentasSpecification(int pageSize, int pageNumber, string? fechaInicio, string? fechaFin)
        {
            Query.Include(dv => dv.DetalleVentas)
                .Include(c => c.Cliente)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
            {
                // Tratar los valores vacíos como si no hubieran sido proporcionados.
                fechaInicio = null;
                fechaFin = null;
            }
            else
            {
                DateTime parsedInicio;
                DateTime parsedFin;

                if (DateTime.TryParse(fechaInicio, out parsedInicio) && DateTime.TryParse(fechaFin, out parsedFin))
                {

                    // Ajustar el rango para incluir todo el día final
                    parsedFin = parsedFin.AddDays(1);
                    // Usar las fechas convertidas
                    Query.Where(x => x.Created >= parsedInicio && x.Created <= parsedFin);
                }
                else
                {
                    throw new ArgumentException("Debe especificar fechas válidas.");
                }
            }
        }
    }
}
