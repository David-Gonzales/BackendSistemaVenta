using Application.Interfaces;
using Domain.Common;
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
        //Por ejemplo:
        //public DbSet<Cliente> Clientes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUTC;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModify = _dateTime.NowUTC;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //permite a EF Core encontrar todas las clases de configuración en el ensamblado actual y aplicarlas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar todas las configuraciones desde el ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuración directa de propiedades en AuditableBaseEntity
            modelBuilder.Entity<AuditableBaseEntity>(builder =>
            {
                builder.Property(e => e.Created)
                    .HasColumnName("Created")
                    .IsRequired();

                builder.Property(e => e.LastModify)
                    .HasColumnName("LastModify")
                    .IsRequired(false);
            });
        }
    }
}
