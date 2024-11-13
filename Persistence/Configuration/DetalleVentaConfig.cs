using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class DetalleVentaConfig : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("DetalleVenta");

            builder.HasKey(p => new { p.IdVenta, p.IdProducto });

            builder.Property(p => p.Cantidad)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.Estado)
                .HasMaxLength(5)//Lleno o Vacío
                .IsRequired();

            builder.Property(p => p.PrecioUnitario)
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(p => p.Total)
                .HasMaxLength(7)
                .IsRequired();

            //Mapeo de Venta y Producto
            //Venta (N-1)?? Producto (N-1)??
            //builder.HasMany(dv => dv.Ventas)
            //    .WithOne(v => v.DetalleVenta)
            //    .HasForeignKey(v => v.IdDe)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
