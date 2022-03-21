using DigitalMenu.Application.Contracts;
using DigitalMenu.Application.Services;
using DigitalMenu.Domain.Products;
using DigitalMenu.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMenu.Infrastructure.IoC
{
    public static class MainModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Application

            services.AddScoped<IProductAppService, ProductAppService>();

            #endregion

            #region Repositories

            services.AddScoped<IProductRepository, ProductRepository>();

            #endregion
        }
    }
}
