namespace Application.DTOs
{
    public class DashboardDto
    {
        public string? TotalIngresos { get; set; }
        public int TotalVentas { get; set; }
        public int TotalProductos { get; set; }
        public List<VentasSemanaDto> VentasUltimaSemana { get; set; }
    }
}
