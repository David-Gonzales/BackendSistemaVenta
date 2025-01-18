namespace Application.DTOs
{
    public class ProductoConEstadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EstadoProductoSimpleDto> Estados {  get; set; }
    }

}
