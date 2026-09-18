using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OrderService.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddOrderApplication(
         this IServiceCollection services)
        {
            services.AddScoped<
                OrderService.Application.Services.OrderService>();

            services.AddScoped<IOrderService>(sp =>
            {
                var implementation =
                    sp.GetRequiredService<
                        OrderService.Application.Services.OrderService>();

                return Shared.Common.Proxy
                    .ExceptionLoggingProxy<IOrderService>
                    .Create(implementation);
            });

            return services;
        }
    }
}
