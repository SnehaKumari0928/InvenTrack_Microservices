using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext(
       DbContextOptions<OrderDbContext> options)
       : base(options)
        {
        }

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserId)
                    .IsRequired();

                entity.Property(x => x.Status)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(x => x.TotalAmount)
                    .IsRequired()
                    .HasPrecision(18, 2);

                entity.Property(x => x.CreatedAt)
                    .IsRequired();

                entity.HasMany(x => x.Items)
                    .WithOne()
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItems");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.OrderId)
                    .IsRequired();

                entity.Property(x => x.ProductId)
                    .IsRequired();

                entity.Property(x => x.Quantity)
                    .IsRequired();

                entity.Property(x => x.UnitPrice)
                    .IsRequired()
                    .HasPrecision(18, 2);

                entity.Ignore(x => x.TotalPrice);
            });

        }
    }
}
