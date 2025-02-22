﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class VentaConfig : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Venta");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroVenta)
                .HasMaxLength(5);//00001, 00002, etc...

            //builder.Property(p => p.TipoVenta)
            //    .HasMaxLength(6)//Normal - Refill
            //    .IsRequired();

            builder.Property(p => p.TipoPago)
                .HasMaxLength(15)//Efectivo - Contraentrega - Tarjeta
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapear las relaciones que tiene con el cliente (N - 1) y Usuario (N - 1) y la lista de Detalles de Venta (1 - N)

            //Cliente (N - 1)
            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            //Usuario (N - 1)
            builder.HasOne(v => v.Usuario)
                .WithMany(u => u.Ventas)
                .HasForeignKey(v => v.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            //DetalleVenta (1 - N)
            builder.HasMany(v => v.DetalleVentas)
                .WithOne(dv => dv.Venta)
                .HasForeignKey(dv => dv.IdVenta)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
