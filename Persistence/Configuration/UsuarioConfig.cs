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
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Rol (N - 1)
            builder.HasOne(u => u.Rol)       // Un Usuario tiene un Rol
                .WithMany(r => r.Usuarios)   // Un Rol puede estar en múltiples Usuarios
                .HasForeignKey(u => u.IdRol) // FK en Usuario que apunta al Rol
                .OnDelete(DeleteBehavior.Restrict); //Si yo borro un usuario, NO quiero que se borren los Roles

            //Transaccion (1 - N)
            builder.HasMany(u => u.Transacciones)
                .WithOne(t => t.Usuario)
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            //Venta (1 - N)
            builder.HasMany(u => u.Ventas)
                .WithOne(v => v.Usuario)
                .HasForeignKey(v => v.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
