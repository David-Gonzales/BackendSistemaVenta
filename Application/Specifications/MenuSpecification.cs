using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class MenuSpecification :Specification<Menu>
    {
        public MenuSpecification(List<int> menuIds)
        {
            // Filtrar los menús que tengan un Id que esté en la lista menuIds
            Query.Where(m => menuIds.Contains(m.Id));

            // Incluir los submenús (los menús con IdMenuPadre == Id del menú principal)
            Query.Include(m => m.Submenus)
                 .Where(m => m.IdMenuPadre == null);  // Solo menús principales
        }
    }
}
