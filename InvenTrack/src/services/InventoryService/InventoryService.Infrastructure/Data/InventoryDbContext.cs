using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace InventoryService.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories => Set<Inventory>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventories");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.ProductId)
                    .IsRequired();

                entity.Property(x => x.Quantity)
                    .IsRequired();

                entity.Property(x => x.ReservedQuantity)
                    .IsRequired();

                entity.Property(x => x.LowStockThreshold)
                    .IsRequired();

                entity.HasIndex(x => x.ProductId)
                    .IsUnique();
            });
        }
    }
}
