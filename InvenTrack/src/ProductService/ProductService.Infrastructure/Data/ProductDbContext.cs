using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

namespace ProductService.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.SKU)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(x => x.SKU)
                    .IsUnique();

                entity.Property(x => x.Price)
                    .HasPrecision(18, 2);

                entity.Property(x => x.Category)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
