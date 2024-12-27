namespace Domain.Entities
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public required int Cantidad { get; set; }
        public TipoEstado TipoEstado { get; set; }//Lleno o vacío
        public TipoVenta TipoVenta { get; set; } //Normal o Refill
        public required decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        //FK Venta
        public int IdVenta { get; set; }
        public Venta? Venta { get; set; }

        //FK Producto
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
    }
}
