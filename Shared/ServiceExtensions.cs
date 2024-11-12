using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class ServiceExtensions
    {
        public static void AgregarSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeServices, IDateTimeServices>();
        }
    }
}
