using Domain.Common;

namespace Domain.Entities
{
    public  class Venta : AuditableBaseEntity
    {
        public string? NumeroVenta { get; set; }
        public required string TipoVenta { get; set; }  
        public required string TipoPago { get; set; }
        public decimal SubTotal => DetalleVentas?.Sum(detalle => detalle.Total) ?? 0;
        public decimal Total { get; set; }

        //Relación con el Cliente (1 por cada venta) que realizó la compra
        //FK Cliente
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        //Relación con el Usuario (1 por cada venta) que realizó la venta
        //FK Usuario
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        //Relación - Lista de Detalles de venta con esta Venta
        public ICollection<DetalleVenta> DetalleVentas { get; set;} = new List<DetalleVenta>();
    }
}
