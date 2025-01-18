using Domain.Entities;

namespace Application.DTOs
{
    public class TransaccionDto
    {
        public int Id { get; set; }
        public string TipoTransaccion { get; set; } //Ingreso o Salida
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string TipoEstado { get; set; } //Lleno o vacío
        //Producto
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CapacidadProducto { get; set; }
        public string UnidadProducto { get; set; }
        //Usuario
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
    }
}
