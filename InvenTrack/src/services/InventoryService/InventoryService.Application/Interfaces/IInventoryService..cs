using InventoryService.Application.DTOs;
using InventoryService.Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Interfaces
{
    public interface IInventoryService
    {

        Task<InventoryDto> CreateAsync(
      CreateInventoryRequest request);

        Task<IEnumerable<InventoryDto>> GetAllAsync();

        Task<InventoryDto> GetByIdAsync(Guid id);

        Task<InventoryDto> GetByProductIdAsync(Guid productId);

        Task AddStockAsync(
            Guid productId,
            StockAdjustmentRequest request);

        Task RemoveStockAsync(
            Guid productId,
            StockAdjustmentRequest request);

        Task ReserveStockAsync(
            Guid productId,
            StockAdjustmentRequest request);

        Task ReleaseStockAsync(
            Guid productId,
            StockAdjustmentRequest request);

        Task DeleteAsync(Guid id);
    }
}
