using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Data;
using InventoryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInventoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("InventoryServiceDb")
                ?? throw new InvalidOperationException(
                    "InventoryDatabase connection string missing")));

            services.AddScoped<IInventoryRepository, InventoryRepository>();

            return services;
        }
    }
}
