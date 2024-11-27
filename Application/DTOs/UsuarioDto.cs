namespace Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool EsActivo { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

    }
}
