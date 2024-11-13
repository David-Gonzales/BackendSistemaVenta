using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class MenuRolConfig : IEntityTypeConfiguration<MenuRol>
    {
        public void Configure(EntityTypeBuilder<MenuRol> builder)
        {
            builder.ToTable("MenuRol");

            // Definino la clave primaria compuesta, mi Id que viene de la auditoría seguirá existiendo pero ya no como PK
            builder.HasKey(mr => new { mr.IdMenu, mr.IdRol });

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Menu (N - 1) 
            builder.HasOne(mr => mr.Menu)
               .WithMany(m => m.MenuRoles)
               .HasForeignKey(mr => mr.IdMenu)
               .OnDelete(DeleteBehavior.Restrict); //Si elimino un registro de MenuRol NO quiero que se eliminen los Menús


            //Rol (N - 1)
            builder.HasOne(mr => mr.Rol)
               .WithMany(r => r.MenuRoles)
               .HasForeignKey(mr => mr.IdRol)
               .OnDelete(DeleteBehavior.Restrict); //Si elimino un registro de MenuRol NO quiero que se eliminen los Roles
        }
    }
}
