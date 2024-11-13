using Domain.Common;

namespace Domain.Entities
{
    public class MenuRol : AuditableBaseEntity
    {
        //FK - Rol
        public int IdRol { get; set; }
        public required Rol Rol { get; set; }

        //FK - Menu
        public int IdMenu { get; set; }
        public required Menu Menu { get; set; }
    }
}
