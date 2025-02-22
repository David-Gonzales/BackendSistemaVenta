﻿using Domain.Entities;

namespace Application.DTOs
{
    public class DetalleVentaDto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public string TipoEstado { get; set; }  // Lleno o vacío
        public string TipoVenta { get; set; } // Normal o Refill
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CapacidadProducto { get; set; }
        public string UnidadProducto { get; set; }
    }
}
