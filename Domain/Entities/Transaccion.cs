using Domain.Common;

namespace Domain.Entities
{
    public class Transaccion : AuditableBaseEntity
    {
        public TipoTransaccion Tipo { get; set; } //Ingreso o Salida
        public required DateTime Fecha { get; set; }
        public required int Cantidad { get; set; }
        public required string Estado { get; set; }//Lleno o vacío

        //FK Producto
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }

        //FK Usuario
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
