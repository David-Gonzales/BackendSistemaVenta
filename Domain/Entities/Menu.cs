using Domain.Common;

namespace Domain.Entities
{
    public class Menu : AuditableBaseEntity
    {
        public required string Nombre { get; set; }
        public required string Icono { get; set; }
        public required string Url { get; set; }
        public int? IdMenuPadre { get; set; } // Esto es opcional, solo para submenús
        public Menu? MenuPadre { get; set; } // Relación hacia el menú padre
        public List<Menu> Submenus { get; set; } = new List<Menu>(); // Relación hacia los submenús

        //Relación - Lista de Roles con este Menu
        public ICollection<MenuRol>? MenuRoles { get; set; }
    }
}
