using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class MenusPorUsuarioSpecification : Specification<Menu>
    {
        public MenusPorUsuarioSpecification(int idUsuario)
        {
            // Filtrar los menús basados en el rol del usuario
            Query.Where(m => m.MenuRoles
                .Any(mr => mr.Rol.Id == idUsuario));

            // Incluir submenús
            Query.Include(m => m.Submenus);  // Asegúrate de incluir los submenús
        }
    }
}
