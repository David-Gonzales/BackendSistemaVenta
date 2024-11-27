using Domain.Common;

namespace Domain.Entities
{
    public class Usuario : AuditableBaseEntity
    {
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Telefono { get; set; }
        public required string Correo { get; set; }
        public required string Clave { get; set; }
        public bool EsActivo { get; set; }

        //FK Rol (N - 1)
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }

        //(1 - N)
        //Relación inversa - Lista de transacciones (entradas y/o salidas) con este usuario
        public ICollection<Transaccion>? Transacciones { get; set; }
        //Relación - Lista de ventas efectuadas por este usuario
        public ICollection<Venta>? Ventas { get; set; }
    }
}
