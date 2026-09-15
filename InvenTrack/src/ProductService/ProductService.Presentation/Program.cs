
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Infrastructure.Repositories;
using ProductService.Domain.Interfaces;
using Shared.Common.DependencyInjection;
using Serilog;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;

namespace ProductService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddSharedServices(builder.Configuration, "productservice", dbRegistration: sc =>
            {
                sc.AddDbContext<ProductDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductServiceDb") ?? throw new InvalidOperationException("ProductServiceDb connection string missing")));
            });

            builder.Services.AddScoped<ProductService.Application.Services.ProductService>();
            builder.Services.AddScoped<ProductService.Infrastructure.Repositories.ProductRepository>();

            // register proxied interfaces which centralize cross-cutting concerns
            builder.Services.AddScoped<IProductRepository>(sp =>
            {
                var impl = sp.GetRequiredService<ProductService.Infrastructure.Repositories.ProductRepository>();
                return Shared.Common.Proxy.ExceptionLoggingProxy<IProductRepository>.Create(impl);
            });

            builder.Services.AddScoped<IProductService>(sp =>
            {
                var impl = sp.GetRequiredService<ProductService.Application.Services.ProductService>();
                return Shared.Common.Proxy.ExceptionLoggingProxy<IProductService>.Create(impl);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // configure Serilog minimal logger in case SharedServiceContainer is not used
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.Console()
               .CreateLogger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSharedPolicies();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
