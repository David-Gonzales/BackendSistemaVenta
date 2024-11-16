using Domain.Entities;

namespace Application.DTOs
{
    public class TransaccionDto
    {
        public int Id { get; set; }
        public TipoTransaccion TipoTransaccion { get; set; } //Ingreso o Salida
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public TipoEstado TipoEstado { get; set; } //Lleno o vacío
        public int IdProducto { get; set; }
        //public Producto? Producto { get; set; }
        public int IdUsuario { get; set; }
        //public Usuario? Usuario { get; set; }
    }
}
