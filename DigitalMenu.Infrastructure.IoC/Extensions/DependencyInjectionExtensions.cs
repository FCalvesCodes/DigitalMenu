using DigitalMenu.Application.Core.WorkUnits;
using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMenu.Infrastructure.IoC.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            MainModule.ConfigureServices(services);
        }

        public static void UseNHibernate(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureNHiberante(configuration);
        }

        public static void UseUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
