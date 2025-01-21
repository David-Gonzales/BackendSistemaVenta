using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reportes.Queries.GetVentasPorFecha
{
    public class GetVentasPorFechaQuery : IRequest<PagedResponse<List<ReporteVentaPorFechasDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }

        public class GetVentasPorFechaQueryHandler : IRequestHandler<GetVentasPorFechaQuery, PagedResponse<List<ReporteVentaPorFechasDto>>>
        {

            private readonly IRepositoryAsync<Venta> _repositoryVentaAsync;
            private readonly IRepositoryAsync<DetalleVenta> _repositoryDetalleVentaAsync;

            public GetVentasPorFechaQueryHandler(IRepositoryAsync<Venta> repositoryVentaAsync, IRepositoryAsync<DetalleVenta> repositoryDetalleVentaAsync)
            {
                _repositoryVentaAsync = repositoryVentaAsync;
                _repositoryDetalleVentaAsync = repositoryDetalleVentaAsync;
            }

            public async Task<PagedResponse<List<ReporteVentaPorFechasDto>>> Handle(GetVentasPorFechaQuery request, CancellationToken cancellationToken)
            {
                int totalCount = await _repositoryDetalleVentaAsync.CountAsync(new DetalleVentaSpecification());

                DateTime parsedInicio, parsedFin;
                List<ReporteVentaPorFechasDto> resultado = new();

                if (DateTime.TryParse(request.FechaInicio, out parsedInicio) && DateTime.TryParse(request.FechaFin, out parsedFin))
                {
                    parsedFin = parsedFin.AddDays(1); // Incluir todo el día final
                    resultado = await _repositoryVentaAsync.GetAllAsQueryable()
                            .Include(v => v.Cliente)
                            .Include(v => v.DetalleVentas)
                            .ThenInclude(dv => dv.Producto)
                            .Where(v => v.Created >= parsedInicio && v.Created <= parsedFin)
                            .SelectMany( //Utilizamos SelectMany en lugar de Select para proyectar cada DetalleVenta como un elemento individual en el resultado final. Esto permite que cada producto en el detalle de ventas genere una fila separada en el DTO.
                                v => v.DetalleVentas.Select(dv => new ReporteVentaPorFechasDto
                                {
                                    FechaRegistro = v.Created.ToString(),
                                    NumeroVenta = v.NumeroVenta,
                                    TipoPago = v.TipoPago,
                                    Cliente = $"{v.Cliente.Nombres} {v.Cliente.Apellidos}",
                                    Producto = $"{dv.Producto.Nombre} {dv.Producto.Capacidad} {dv.Producto.Unidad}",
                                    Cantidad = dv.Cantidad,
                                    TipoVenta = dv.TipoVenta.ToString(),
                                    TipoEstado = dv.TipoEstado.ToString(),
                                    Precio = dv.PrecioUnitario,
                                    TotalProducto = dv.Cantidad * dv.PrecioUnitario
                                })
                            ).ToListAsync();
                }

                return new PagedResponse<List<ReporteVentaPorFechasDto>>(resultado, request.PageNumber, request.PageSize, totalCount);
            } 
        }
    }
}
