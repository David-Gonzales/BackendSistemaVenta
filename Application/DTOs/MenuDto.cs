using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MenuDto
    {
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public ICollection<MenuRol>? MenuRoles { get; set; }
    }
}
