using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class NumeroVentaConfig : IEntityTypeConfiguration<NumeroVenta>
    {
        public void Configure(EntityTypeBuilder<NumeroVenta> builder)
        {
            builder.ToTable("NumeroVenta");

            builder.HasKey(p => p.IdNumeroVenta);

            builder.Property(p => p.UltimoNumero)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.FechaRegistro);
        }
    }
}
