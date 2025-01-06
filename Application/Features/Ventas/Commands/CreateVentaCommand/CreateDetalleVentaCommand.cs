namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateDetalleVentaCommand
    {
        public int IdProducto { get; set; }
        public required int Cantidad { get; set; }
        public required string TipoEstado { get; set; }//Lleno o vacío
        public required string TipoVenta { get; set; }
        public required decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
