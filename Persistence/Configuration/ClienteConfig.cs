using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombres)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Apellidos)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.TipoDocumento)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(p => p.NumeroDocumento)
                .HasMaxLength(12)
                .IsRequired();

            builder.Property(p => p.Correo)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(p => p.Ciudad)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Telefono)
                .HasMaxLength(9)
                .IsRequired();

            builder.Property(p => p.FechaNacimiento)
                .IsRequired();

            builder.Property(p => p.EsActivo)
                .IsRequired();

            builder.Property(p => p.Edad);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);
            
            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            builder.HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
