using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string SKU { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public string Category { get; private set; } = string.Empty;

        public Guid SupplierId { get; private set; }

        private Product()
        {

        }
        public Product(
          string name,
          string sku,
          string description,
          decimal price,
          string category,
          Guid supplierId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU is required.");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category is required.");

            if (supplierId == Guid.Empty)
                throw new ArgumentException("SupplierId is required.");

            Id = Guid.NewGuid();
            Name = name;
            SKU = sku;
            Description = description;
            Price = price;
            Category = category;
            SupplierId = supplierId;
        }

        public void Update(
            string name,
            string description,
            decimal price,
            string category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category is required.");

            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

    }
}
