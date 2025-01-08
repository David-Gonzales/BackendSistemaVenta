using Application.Parameters;

namespace Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasParameters : RequestParameter
    {
        public string? BuscarPor { get; set; }
        public string? NumeroVenta { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
    }
}
