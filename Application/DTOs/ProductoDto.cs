namespace Application.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Unidad { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public bool EsActivo { get; set; }
    }
}
