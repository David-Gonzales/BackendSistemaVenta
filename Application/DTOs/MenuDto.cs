using Domain.Entities;

namespace Application.DTOs
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public List<MenuDto> Submenus { get; set; } = new List<MenuDto>();  // Inicializado como lista vacía
    }
}
