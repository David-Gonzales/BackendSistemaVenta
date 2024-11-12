using Domain.Common;

namespace Domain.Entities
{
    public class MenuRol : AuditableBaseEntity
    {
        //FK - Rol
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }

        //FK - Menu
        public int IdMenu { get; set; }
        public Menu? Menu { get; set; }
    }
}
