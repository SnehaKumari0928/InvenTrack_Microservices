using Microsoft.EntityFrameworkCore;
using SupplierService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Infrastructure.Data
{
    public class SupplierDbContext: DbContext
    {

        public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers => Set<Supplier>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(x => x.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasIndex(x => x.Email)
                    .IsUnique();
            });
        }
    }
}
