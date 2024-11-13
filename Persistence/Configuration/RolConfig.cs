using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //Relación con la lista de usuarios para este rol y la lista de menus para este Rol
        }
    }
}
