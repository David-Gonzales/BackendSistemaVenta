namespace Application.DTOs
{
    public class CambiosDto
    {
        public bool CambioProducto { get; set; }
        public bool CambioEstado { get; set; }
        public bool CambioCantidad { get; set; }
        public bool SoloCambioCantidad { get; set; }
        public bool HayCambios { get; set; }
    }
}
