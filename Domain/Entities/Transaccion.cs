using Domain.Common;

namespace Domain.Entities
{
    public class Transaccion : AuditableBaseEntity
    {
        public TipoTransaccion TipoTransaccion { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public TipoEstado TipoEstado { get; set; }//Lleno o vacío

        //Entity Framework infiere la relación automáticamente, siempre que se declare tanto la propiedad de navegación (Producto, Usuario) como la clave foránea (IdProducto, IdUsuario)
        //FK Producto
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; } //propiedad de navegación

        //FK Usuario
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; } //propiedad de navegación

    }
}
