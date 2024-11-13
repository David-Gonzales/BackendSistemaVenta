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
    public class TransaccionConfig : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transaccion");

            builder.HasKey(p => p.Id);

            builder.Property(t => t.Tipo)
                .HasConversion<int>() //0:Entrada - 1:Salida
                .IsRequired();

            builder.Property(p => p.Fecha)
                .IsRequired();

            builder.Property(p => p.Cantidad)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Estado)
                .HasMaxLength(5)//Lleno - Vacío
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapear las relaciones con 1 o más Producto y mapear las relaciones que se tiene con el usuario que lo ingresa (1-1)

            //Producto (N - 1)??

            //Usuario (N - 1)??
        }
    }
}
