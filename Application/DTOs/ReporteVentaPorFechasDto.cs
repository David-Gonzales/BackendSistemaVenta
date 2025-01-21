namespace Application.DTOs
{
    public class ReporteVentaPorFechasDto
    {
        public string FechaRegistro { get; set; }
        public string NumeroVenta { get; set; }
        public string TipoPago { get; set; }
        public string Cliente { get; set; } //Nombres y apellidos
        public string Producto { get; set; } //Nombre + Capacidad + Unidad
        public int Cantidad { get; set; }
        public string TipoVenta { get; set; }
        public string TipoEstado { get; set; }
        public decimal Precio { get; set; }
        public decimal TotalProducto { get; set; }
    }
}
