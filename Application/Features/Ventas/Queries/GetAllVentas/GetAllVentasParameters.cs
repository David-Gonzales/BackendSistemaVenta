using Application.Parameters;

namespace Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasParameters : RequestParameter
    {
        public string? NumeroVenta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
