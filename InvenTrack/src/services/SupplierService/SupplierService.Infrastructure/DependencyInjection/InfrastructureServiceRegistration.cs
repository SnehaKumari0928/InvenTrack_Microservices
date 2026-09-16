using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplierService.Domain.Interfaces;
using SupplierService.Infrastructure.Data;
using SupplierService.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddSupplierInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<SupplierDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("SupplierServiceDb")));

            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }

    }
}
