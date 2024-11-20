using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasQuery : IRequest<PagedResponse<List<HistorialVentaDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? NumeroVenta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public class GetAllVentasQueryHandler : IRequestHandler<GetAllVentasQuery, PagedResponse<List<HistorialVentaDto>>>
        {
            private readonly IRepositoryAsync<Venta> _repositoryVentaAsync;
            private readonly IRepositoryAsync<Cliente> _repositoryClienteAsync;
            private readonly IRepositoryAsync<DetalleVenta> _repositoryDetalleVentaAsync;

            public GetAllVentasQueryHandler(IRepositoryAsync<Venta> repositoryVentaAsync, IRepositoryAsync<Cliente> repositoryClienteAsync, IRepositoryAsync<DetalleVenta> repositoryDetalleVentaAsync)
            {
                _repositoryVentaAsync = repositoryVentaAsync;
                _repositoryClienteAsync = repositoryClienteAsync;
                _repositoryDetalleVentaAsync = repositoryDetalleVentaAsync;
            }

            public async Task<PagedResponse<List<HistorialVentaDto>>> Handle(GetAllVentasQuery request, CancellationToken cancellationToken)
            {
                var ventas = await _repositoryVentaAsync.ListAsync(new PagedVentasSpecification(request.PageSize, request.PageNumber, request.NumeroVenta, request.FechaInicio, request.FechaFin));

                var clientes = await _repositoryClienteAsync.ListAsync();
                var detalleVenta = await _repositoryDetalleVentaAsync.ListAsync(new DetalleVentaSpecification(), cancellationToken);

                var resultado = ventas.Select(v=> new HistorialVentaDto
                {
                    Id = v.Id,
                    NumeroVenta = v.NumeroVenta,
                    TipoVenta = v.TipoVenta,
                    TipoPago = v.TipoPago,
                    Total = v.Total,

                    IdCliente = clientes.FirstOrDefault(c => c.Id == v.IdCliente).Id,
                    NombreCliente = clientes.FirstOrDefault(c => c.Id == v.IdCliente)?.Nombres,
                    ApellidosCliente = clientes.FirstOrDefault(c => c.Id == v.IdCliente)?.Apellidos,

                    DetalleVentas = detalleVenta.Where(dv => dv.IdVenta == v.Id)
                    .Select(dv => new DetalleVentaDto
                    {
                        Id = dv.Id,
                        Cantidad = dv.Cantidad,
                        PrecioUnitario = dv.PrecioUnitario,
                        Total = dv.Total,
                        IdProducto = dv.IdProducto,
                        NombreProducto = dv.Producto.Nombre,
                        CapacidadProducto = dv.Producto.Capacidad,
                        UnidadProducto = dv.Producto.Unidad
                    }).ToList()
                }).ToList();

                return new PagedResponse<List<HistorialVentaDto>>(resultado, request.PageNumber, request.PageSize);
            }
        }
    }
}
