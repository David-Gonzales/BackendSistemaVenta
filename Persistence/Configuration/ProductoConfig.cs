﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ProductoConfig : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Producto");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Capacidad)
                .IsRequired();

            builder.Property(p => p.Unidad)
                .HasMaxLength(3)
                .IsRequired();

            //builder.Property(p => p.Stock)
            //    .IsRequired();

            builder.Property(p => p.Precio)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder.Property(p => p.EsActivo)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapeo de la relación que tiene con DetalleVenta y Transaccion
            //DetalleVenta (1 - N)
            builder.HasMany(p => p.DetalleVentas)
                .WithOne(dv => dv.Producto)
                .HasForeignKey(dv => dv.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            //Transaccion (1 - N)
            builder.HasMany(p => p.Transacciones)
                .WithOne(t => t.Producto)
                .HasForeignKey(t => t.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            //Transaccion (1 - N)
            builder.HasMany(p => p.Estados)
                .WithOne(t => t.Producto)
                .HasForeignKey(t => t.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
