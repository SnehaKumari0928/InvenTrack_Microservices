
using InventoryService.Application.DependencyInjection;
using InventoryService.Infrastructure.DependencyInjection;
using Shared.Common.DependencyInjection;

namespace InventoryService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddInventoryApplication();

            builder.Services.AddInventoryInfrastructure(
                builder.Configuration);

            builder.Services.AddSharedServices(
                builder.Configuration,
                "inventoryservice");

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

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
