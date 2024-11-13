using Domain.Common;

namespace Domain.Entities
{
    public class Rol : AuditableBaseEntity
    {
        public required string Nombre { get; set; }

        //Relación inversa - Lista de usuarios con este rol
        public ICollection<Usuario>? Usuarios { get; set; }
        //Relación - Lista de Menus con este rol
        public ICollection<MenuRol>? MenuRoles { get; set; }
    }
}
