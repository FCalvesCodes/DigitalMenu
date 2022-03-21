using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate.Extensions
{
    public static class NHibernateExtensions
    {
        public static void ConfigureNHiberante(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<INhSessionFactoryBuilder, FluentSessionFactoryBuilder>();
            services.AddSingleton((c) => c.GetRequiredService<INhSessionFactoryBuilder>().BuildSessionFactory());
            services.AddScoped<ITransactionManager, NhTransactionManager>();
            services.AddScoped<IDataContext, NhDataContext>();
            services.AddScoped<INhDataContext, NhDataContext>();
            services.AddScoped<INhSessionProvider, NhSessionProvider>();
            services.Configure<NhibernateOptions>(options => configuration.GetSection("NHibernate").Bind(options));
        }
    }
}
