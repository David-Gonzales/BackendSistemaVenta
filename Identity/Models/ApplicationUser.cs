using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
    }
}
