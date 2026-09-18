using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Clients;
using System;

namespace OrderService.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddOrderInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IInventoryClient, InventoryClient>(client =>
            {
                var baseUrl = configuration["Services:InventoryService"]
                    ?? configuration["InventoryServiceUrl"]
                    ?? "http://localhost:5000";

                client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
            });

            services.AddHttpClient<ISupplierClient, SupplierClient>(client =>
            {
                var baseUrl = configuration["Services:SupplierService"]
                    ?? configuration["SupplierServiceUrl"]
                    ?? "http://localhost:5002";

                client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
            });

            return services;
        }
    }
}
