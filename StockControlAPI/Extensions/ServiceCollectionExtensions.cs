using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Profiles;
using StockControlAPI.Infrastruture.Data;
using StockControlAPI.Service.Interfaces;
using StockControlAPI.Service.Repositories;
using StockControlAPI.Service.Service;

namespace StockControlAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockControlAPI", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockControlAPIContext>(options =>
                options.UseInMemoryDatabase("StockControlAPIInMemory"));

            return services;
        }

        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBaseValidator<ProductDto>, ProductValidator>();

            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IBaseValidator<StockDto>, StockValidator>();

            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}