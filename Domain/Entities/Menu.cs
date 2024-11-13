using Domain.Common;

namespace Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        public required string Nombre { get; set; }
        public required string Icono { get; set; }
        public required string Url { get; set; }

        //Relación - Lista de Roles con este Menu
        public ICollection<MenuRol>? MenuRols { get; set; }
    }
}
