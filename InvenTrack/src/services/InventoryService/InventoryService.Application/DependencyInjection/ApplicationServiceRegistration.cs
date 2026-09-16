using InventoryService.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddInventoryApplication(this IServiceCollection services)
        {
            services.AddScoped<InventoryService.
                Application.Services.InventoryService>();

            services.AddScoped<IInventoryService>(sp =>
            {
                var implementation =
                    sp.GetRequiredService<
                        InventoryService.Application.Services.InventoryService>();

                return Shared.Common.Proxy
                    .ExceptionLoggingProxy<IInventoryService>
                    .Create(implementation);
            });

            return services;
        }
    }
}
