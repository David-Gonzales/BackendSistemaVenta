using Application.DTOs;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Reportes.Queries.GetVentasPorCliente
{
    public class GetVentasPorClienteQuery : IRequest<PagedResponse<List<ReporteVentaPorFechasDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
