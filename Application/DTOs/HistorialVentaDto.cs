namespace Application.DTOs
{
    public class HistorialVentaDto
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NumeroVenta { get; set; }
        //public string TipoVenta { get; set; }
        public string TipoPago { get; set; }
        public decimal Total { get; set; }

        //Cliente
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }

        //Detalle Venta
        public ICollection<DetalleVentaDto> DetalleVentas { get; set; }
    }
}
