using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Configuration
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Icono)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Url)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
            .HasMaxLength(50);

            //builder.HasOne(m => m.MenuPadre)
            //    .WithMany()
            //    .HasForeignKey(m => m.IdMenuPadre)
            //    .OnDelete(DeleteBehavior.NoAction);

            //MenuRol (1 - N)
            builder.HasMany(m => m.MenuRoles)
                .WithOne(mr => mr.Menu)
                .HasForeignKey(mr => mr.IdMenu)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
