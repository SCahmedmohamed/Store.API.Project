using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.About_Caches;
using Services.Abstractions;
using Services.Abstractions.About_Caches;
using Services.Mapping.About_Baskets;
using Services.Mapping.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));

            return services;
        }
    }
}
