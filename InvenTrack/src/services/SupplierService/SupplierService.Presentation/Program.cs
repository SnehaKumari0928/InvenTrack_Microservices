
using Shared.Common.DependencyInjection;
using SupplierService.Application.DependencyInjection;
using SupplierService.Infrastructure.DependencyInjection;

namespace SupplierService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddSupplierApplication();

            builder.Services.AddSupplierInfrastructure(
                builder.Configuration);

            builder.Services.AddSharedServices(
                builder.Configuration,
                "supplierservice");

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
