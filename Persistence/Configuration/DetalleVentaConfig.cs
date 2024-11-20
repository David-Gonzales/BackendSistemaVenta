using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class DetalleVentaConfig : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("DetalleVenta");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Cantidad)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.TipoEstado)
                .HasConversion<string>()//Lleno o Vacío
                .IsRequired();

            builder.Property(p => p.PrecioUnitario)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            
            //Venta (N-1)
            
            builder.HasOne(dv => dv.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(v => v.IdVenta)
                .OnDelete(DeleteBehavior.Cascade); //Si borro un detalle de venta se borrará la venta (algo que nunca va a pasar (regla del negocio, las ventas no pueden ser eliminadas o modificado una vez que se hayan creado, por ende tampoco deberían ser eliminadas o modificadas sus detalles de venta) a no ser que sea a través de BD, si eso pasara quiero que se elimine con toda la venta, para que no quede una venta sin su detalle (sería raro una venta sin detalle de venta))
            
            //Producto (N-1)

            builder.HasOne(dv => dv.Producto)
                .WithMany(p => p.DetalleVentas)
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Restrict); //Si borro un detalle de venta NO quiero que se borre el Producto (algo que no va a pasar dado a la regla de negocio). Si llegara a pasar que desde la BD borro (intencional o por error) el Detalle de Venta NO quiero que se borren mis productos (Sería catastrófico que se borrara un producto solo por eliminar 1 detalle de venta).
            
        }
    }
}
