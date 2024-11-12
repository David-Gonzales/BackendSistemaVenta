using Domain.Common;

namespace Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        public string? Nombre { get; set; }
        public string? Icono { get; set; }
        public string? Url { get; set; }

        //Relación - Lista de Roles con este Menu
        public ICollection<MenuRol>? MenuRols { get; set; }
    }
}
