﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AgregarPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName
                    )
            ));

            #region Repositorios
            //Matriculamos el patrón repositorio
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            services.AddTransient<IReadRepositoryAsync<Producto>, MyRepositoryAsync<Producto>>();

            //Matriculo el repositorio de usuario (inicio de sesión)
            services.AddTransient<IUsuarioRepositoryAsync, UsuarioRepositoryAsync>();
            
            //Matriculo el UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Matriculo el servicio de EstadoProducto
            services.AddScoped<IEstadoProductoService, EstadoProductoService>();
            #endregion
        }
    }
}
