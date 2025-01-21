using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Features.Dashboard.Queries.GetDashboard
{
    public class GetDashBoardQuery : IRequest<Response<DashboardDto>>
    {
        public class GetDashBoardQueryHandler: IRequestHandler<GetDashBoardQuery, Response<DashboardDto>> 
        {
            private readonly IRepositoryAsync<Venta> _repositoryVentaAsync;
            private readonly IRepositoryAsync<Producto> _repositoryProductoAsync;

            public GetDashBoardQueryHandler(IRepositoryAsync<Venta> repositoryVentaAsync, IRepositoryAsync<Producto> repositoryProductoAsync)
            {
                _repositoryVentaAsync = repositoryVentaAsync;
                _repositoryProductoAsync = repositoryProductoAsync;
            }

            public async Task<Response<DashboardDto>> Handle(GetDashBoardQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Venta> tablaVenta = _repositoryVentaAsync.GetAllAsQueryable();

                // Total de ventas y conteo de ventas
                var totalVentas = await tablaVenta.SumAsync(v => v.Total, cancellationToken);
                var conteoVentas = await tablaVenta.CountAsync(cancellationToken);

                // Fecha de inicio para calcular la última semana
                var fechaInicio = DateTime.Now.AddDays(-7).Date;

                // Ventas agrupadas por fecha en la última semana
                var ventasUltimaSemana = await tablaVenta
                    .Where(v => v.Created >= fechaInicio)
                    .GroupBy(v => v.Created.Date)
                    .Select(g => new VentasSemanaDto
                    {
                        Fecha = g.Key.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Total = g.Count()
                    })
                    .ToListAsync(cancellationToken);

                // Total de ingresos en la última semana
                var totalIngresos = await tablaVenta
                    .Where(v => v.Created >= fechaInicio)
                    .SumAsync(v => v.Total, cancellationToken);

                // Total de productos
                var totalProductos = await _repositoryProductoAsync.GetAllAsQueryable().CountAsync(cancellationToken);

                // Construcción del DTO de Dashboard
                var dashboardDto = new DashboardDto
                {
                    TotalIngresos = totalIngresos.ToString("N2", new CultureInfo("es-PE")),
                    TotalVentas = ventasUltimaSemana.Sum(v => v.Total),
                    TotalProductos = totalProductos,
                    VentasUltimaSemana = ventasUltimaSemana
                };

                return new Response<DashboardDto>(dashboardDto);
            }
        }
    }
}
