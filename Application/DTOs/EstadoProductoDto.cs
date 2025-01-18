namespace Application.DTOs
{
    public class EstadoProductoDto
    {
        public required string TipoEstado { get; set; } // Por ejemplo, "Lleno" o "Vacío"
        public int Stock { get; set; }

        //Producto
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CapacidadProducto { get; set; }
        public string UnidadProducto { get; set; }
    }
}
