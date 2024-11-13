using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            //Mapear la relación que tiene con los roles
        }
    }
}
