using Domain.Common;

namespace Domain.Entities
{
    public class EstadoProducto : AuditableBaseEntity
    {
        public TipoEstado TipoEstado { get; set; } // Por ejemplo, "Lleno" o "Vacío"
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; } //propiedad de navegación
    }
}
