using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class RolConfig : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Usuarios (1 - N)
            builder.HasMany(r => r.Usuarios)
                .WithOne(u => u.Rol)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);

            //MenuRol (1 - N)
            builder.HasMany(m => m.MenuRoles)
                .WithOne(mr => mr.Rol)
                .HasForeignKey(mr => mr.IdRol)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
