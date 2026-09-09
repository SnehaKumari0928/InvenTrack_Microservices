using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.ProductService.Domain.Entities;

namespace ProductService.ProductService.Infrastructure.Persistence.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.SKU)
                .IsUnique();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
