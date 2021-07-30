using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Net5Api.Context;
using Net5Api.ProductMaster;
using System;

namespace Net5Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
        }

        public static void AddEssentials(this IServiceCollection services)
        {
            services.RegisterSwagger();
            services.AddVersioning();
        }

        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //TODO - Lowercase Swagger Documents
                //c.DocumentFilter<LowercaseDocumentFilter>();
                //Refer - https://gist.github.com/rafalkasa/01d5e3b265e5aa075678e0adfd54e23f
               c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Net5Api",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });
        }

        private static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>
          (options =>
          {
              options
                .UseNpgsql(configuration.GetConnectionString("ApplicationConnection"), b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)
                .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
          });
        }

        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductDbContext, ProductDbContext>();
        }

        public static void AddPersistenceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
    
        }

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}