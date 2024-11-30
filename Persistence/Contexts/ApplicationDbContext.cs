using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeServices _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeServices dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        //DbSets
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuRol> MenuRol { get; set; }
        public DbSet<NumeroVenta> NumeroVenta { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Transaccion> Transaccion { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venta> Venta { get; set; }
        
        

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowPeru;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModify = _dateTime.NowPeru;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<DetalleVenta>())
            {
                // Si es una nueva venta, calculamos el Total
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Total = entry.Entity.Cantidad * entry.Entity.PrecioUnitario;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //permite a EF Core encontrar todas las clases de configuración en el ensamblado actual y aplicarlas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar todas las configuraciones desde el ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
