using Microsoft.Extensions.DependencyInjection;
using SupplierService.Application.Interfaces;
using SupplierService.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<SupplierService.Application.Services.SupplierService>();

            services.AddScoped<ISupplierService>(sp =>
            {
                var implementation =
                    sp.GetRequiredService<SupplierService.Application.Services.SupplierService>();

                return Shared.Common.Proxy.ExceptionLoggingProxy<ISupplierService>
                    .Create(implementation);
            });
            return services;
        }

        // Backwards-compatible helper with service-specific name used by Presentation projects
        public static IServiceCollection AddSupplierApplication(this IServiceCollection services)
        {
            return services.AddApplicationServices();
        }
    }
}
