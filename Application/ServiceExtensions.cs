using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public  static class ServiceExtensions
    {
        public static void AgregarCapaAplicacion(this IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
