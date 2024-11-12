﻿using Domain.Common;

namespace Domain.Entities
{
    public  class Venta : AuditableBaseEntity
    {
        public string? NumeroVenta { get; set; }
        public required string TipoVenta { get; set; }  
        public required string TipoPago { get; set; }
        public decimal Total => DetalleVenta?.Sum(detalle => detalle.Total) ?? 0;

        //Relación con el Cliente (1 por cada venta) que realizó la compra
        //FK Cliente
        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        //Relación con el Usuario (1 por cada venta) que realizó la venta
        //FK Usuario
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        //Relación - Lista de Detalles de venta con esta Venta
        public ICollection<DetalleVenta> DetalleVenta { get; set;} = new List<DetalleVenta>();
    }
}
