using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombres)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Apellidos)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Telefono)
                .HasMaxLength(9)
                .IsRequired();

            builder.Property(p => p.Correo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Clave)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.EsActivo)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapear las realaciones que se tiene con el rol para ese usuario y la lista de transacciones que haya hecho (1-N) y la lista de ventas que haya hecho (1-N)
        }
    }
}
