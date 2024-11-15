using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class TransaccionConfig : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transaccion");

            builder.HasKey(p => p.Id);

            builder.Property(t => t.TipoTransaccion)
                .HasConversion<string>() //Entrada - Salida
                .IsRequired();

            builder.Property(p => p.Fecha)
                .IsRequired();

            builder.Property(p => p.Cantidad)
                .IsRequired();

            builder.Property(p => p.TipoEstado)
                .HasConversion<string>()//Lleno o Vacío
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(50);

            builder.Property(p => p.LastModifyBy)
                .HasMaxLength(50);

            //Mapear las relaciones con 1 o más Producto y mapear las relaciones que se tiene con el usuario que lo ingresa (1-1)

            //Producto (N - 1)??
            builder.HasOne(t => t.Producto)
                .WithMany(p => p.Transacciones)
                .HasForeignKey(v => v.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            //Usuario (N - 1)??
            builder.HasOne(t => t.Usuario)
                .WithMany(u => u.Transacciones)
                .HasForeignKey(v => v.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
