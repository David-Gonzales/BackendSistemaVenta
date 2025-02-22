﻿using Domain.Common;

namespace Domain.Entities
{
    public class Producto : AuditableBaseEntity
    {
        public required string Nombre { get; set; }
        public required int Capacidad { get; set; }
        public required string Unidad { get; set; }
        //public required int Stock { get; set; }
        public required decimal Precio { get; set; }
        public bool EsActivo { get; set; }

        //Relación - Lista de DetallesVenta de cada Producto
        public ICollection<DetalleVenta>? DetalleVentas { get; set; }

        //Relación - Lista de Transacciones de cada Producto
        public ICollection<Transaccion>? Transacciones { get; set; }

        //Relación - Lista de Estados de cada producto (Lleno y vacío por ahora, más adelante podrían ser Dañado o En Reparación)
        public required ICollection<EstadoProducto> Estados { get; set; }
    }
}
