namespace SGDE.API.Configurations
{
    #region Using

    using Domain.Repositories;
    using Domain.Supervisor;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;
    using Domain.Helpers;
    using Models;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureSection = configuration.GetSection("Infrastructure");
            services.Configure<InfrastructureAppSettings>(infrastructureSection);
            var infrastructure = infrastructureSection.Get<InfrastructureAppSettings>();

            switch (infrastructure.Type)
            {
                case "SQL":
                    services.AddScoped<IOrderRepository, DataEFCoreSQL.Repositories.OrderRepository>();

                    break;
                case "MongoDB":
                    services.AddScoped<IOrderRepository, DataEFCoreMongoDB.Repositories.OrderRepository>();

                    break;

                default:
                    services.AddScoped<IOrderRepository, DataEFCoreSQL.Repositories.OrderRepository>();

                    break;
            }

            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<ISupervisor, Supervisor>();

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = new ReferenceLoopHandling());

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build());
            });
    }
}