using InventoryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(Guid id);

        Task<Inventory?> GetByProductIdAsync(Guid productId);

        Task<IEnumerable<Inventory>> GetAllAsync();

        Task AddAsync(Inventory inventory);

        Task UpdateAsync(Inventory inventory);

        Task DeleteAsync(Inventory inventory);
    }
}
