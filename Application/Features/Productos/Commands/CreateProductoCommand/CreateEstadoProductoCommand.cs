namespace Application.Features.Productos.Commands.CreateProductoCommand
{
    public class CreateEstadoProductoCommand
    {
        public string? TipoEstado { get; set; } // Por ejemplo, "Lleno" o "Vacío"
        public int Stock { get; set; }
    }
}
