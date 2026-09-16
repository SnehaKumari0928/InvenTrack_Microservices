using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Domain.Entities
{
    public class Inventory
    {

        public Guid Id { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public int ReservedQuantity { get; private set; }

        public int LowStockThreshold { get; private set; }

        private Inventory()
        {
        }

        public Inventory(
            Guid productId,
            int quantity,
            int lowStockThreshold)
        {
            Validate(productId, quantity, lowStockThreshold);

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            ReservedQuantity = 0;
            LowStockThreshold = lowStockThreshold;
        }

        public int AvailableQuantity =>
            Quantity - ReservedQuantity;

        public bool IsLowStock =>
            AvailableQuantity <= LowStockThreshold;

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Stock quantity must be greater than zero.");

            Quantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Stock quantity must be greater than zero.");

            if (quantity > AvailableQuantity)
                throw new InvalidOperationException(
                    "Insufficient available stock.");

            Quantity -= quantity;
        }

        public void ReserveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Reservation quantity must be greater than zero.");

            if (quantity > AvailableQuantity)
                throw new InvalidOperationException(
                    "Insufficient available stock.");

            ReservedQuantity += quantity;
        }

        public void ReleaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Release quantity must be greater than zero.");

            if (quantity > ReservedQuantity)
                throw new InvalidOperationException(
                    "Cannot release more stock than reserved.");

            ReservedQuantity -= quantity;
        }

        private static void Validate(
            Guid productId,
            int quantity,
            int lowStockThreshold)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(
                    "ProductId is required.");

            if (quantity < 0)
                throw new ArgumentException(
                    "Quantity cannot be negative.");

            if (lowStockThreshold < 0)
                throw new ArgumentException(
                    "Low stock threshold cannot be negative.");
        }
    }
}
