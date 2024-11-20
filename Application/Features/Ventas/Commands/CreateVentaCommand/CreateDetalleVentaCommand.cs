using Domain.Entities;

namespace Application.Features.Ventas.Commands.CreateVentaCommand
{
    public class CreateDetalleVentaCommand
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string TipoEstado { get; set; }//Lleno o vacío
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
