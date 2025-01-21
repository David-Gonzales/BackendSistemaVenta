using Application.Parameters;

namespace Application.Features.Reportes.Queries.GetVentasPorCliente
{
    public class GetVentasPorClienteParameters : RequestParameter
    {
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
    }
}
