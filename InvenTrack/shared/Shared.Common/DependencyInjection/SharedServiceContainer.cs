using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Common.Middleware;
using System;
using System.IO;

namespace Shared.Common.DependencyInjection
{
    public static class SharedServiceContainer
    {
        /// <summary>
        /// Adds shared services including logging, JWT authentication and an optional database registration callback.
        /// The dbRegistration action can register any DbContext or provider and is executed by the caller.
        /// </summary>
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config, string fileName, Action<IServiceCollection>? dbRegistration = null)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (config is null) throw new ArgumentNullException(nameof(config));

            // Allow the caller to register database-related services with full control
            dbRegistration?.Invoke(services);

            // Shared configuration
            ConfigureSerilog(fileName);

            // Add JWT authentication Scheme
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, config);

            // Common helpers
            services.AddHttpContextAccessor();

            return services;
        }

        private static void ConfigureSerilog(string? fileName)
        {
            fileName ??= "application";
            // ensure extension
            var logFile = fileName.EndsWith(".log", StringComparison.OrdinalIgnoreCase) ? fileName : fileName + ".log";

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Debug()
               .WriteTo.Console()
               .WriteTo.File(path: logFile,
                   restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                   rollingInterval: RollingInterval.Day)
               .CreateLogger();
        }

        /// <summary>
        /// Use shared middleware policies such as GlobalException and Api-Gateway listener.
        /// </summary>
        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalException>();
            app.UseMiddleware<ListenToOnlyApiGateway>();
            return app;
        }
    }
}
