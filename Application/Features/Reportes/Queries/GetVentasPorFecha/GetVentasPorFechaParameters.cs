using Application.Parameters;

namespace Application.Features.Reportes.Queries.GetVentasPorFecha
{
    public class GetVentasPorFechaParameters : RequestParameter
    {
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
    }
}
