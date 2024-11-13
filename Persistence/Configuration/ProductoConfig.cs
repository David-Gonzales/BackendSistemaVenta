using Domain.Entities;
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
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Unidad)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.Stock)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Precio)
                .HasMaxLength (7)
                .IsRequired();

            builder.Property(p => p.EsActivo)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapeo de la relación que tiene con DetalleVenta
            //DetalleVenta (1 - N)
            builder.HasMany(p => p.DetalleVenta)
                .WithOne(dv => dv.Producto)
                .HasForeignKey(dv => dv.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
