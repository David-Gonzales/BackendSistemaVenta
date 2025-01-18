using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class EstadoProductoConfig : IEntityTypeConfiguration<EstadoProducto>
    {
        public void Configure(EntityTypeBuilder<EstadoProducto> builder)
        {
            builder.ToTable("EstadoProducto");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.TipoEstado)
                .HasConversion<string>()//Lleno o Vacío
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Producto (N - 1)
            builder.HasOne(t => t.Producto)
                .WithMany(p => p.Estados)
                .HasForeignKey(v => v.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
