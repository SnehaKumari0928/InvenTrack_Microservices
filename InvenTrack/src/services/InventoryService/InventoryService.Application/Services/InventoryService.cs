using InventoryService.Application.DTOs;
using InventoryService.Application.Interfaces;
using InventoryService.Application.Requests;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Interfaces;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<InventoryDto> CreateAsync(
        CreateInventoryRequest request)
        {
            var existing =
                await _repository.GetByProductIdAsync(request.ProductId);

            if (existing is not null)
            {
                throw new ValidationException(
                    $"Inventory already exists for product '{request.ProductId}'.");
            }

            var inventory = new Inventory(
                request.ProductId,
                request.Quantity,
                request.LowStockThreshold);

            await _repository.AddAsync(inventory);

            return MapToDto(inventory);
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();

            return inventories.Select(MapToDto);
        }

        public async Task<InventoryDto> GetByIdAsync(Guid id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory is null)
                throw new NotFoundException();

            return MapToDto(inventory);
        }

        public async Task<InventoryDto> GetByProductIdAsync(
       Guid productId)
        {
            var inventory =
                await _repository.GetByProductIdAsync(productId);

            if (inventory is null)
                throw new NotFoundException();

            return MapToDto(inventory);
        }

        public async Task AddStockAsync(
        Guid productId,
        StockAdjustmentRequest request)
        {
            var inventory =
                await _repository.GetByProductIdAsync(productId);

            if (inventory is null)
                throw new NotFoundException();

            inventory.AddStock(request.Quantity);

            await _repository.UpdateAsync(inventory);
        }

        public async Task RemoveStockAsync(
        Guid productId,
        StockAdjustmentRequest request)
        {
            var inventory =
                await _repository.GetByProductIdAsync(productId);

            if (inventory is null)
                throw new NotFoundException();

            inventory.RemoveStock(request.Quantity);

            await _repository.UpdateAsync(inventory);
        }

        public async Task ReserveStockAsync(
       Guid productId,
       StockAdjustmentRequest request)
        {
            var inventory =
                await _repository.GetByProductIdAsync(productId);

            if (inventory is null)
                throw new NotFoundException();

            inventory.ReserveStock(request.Quantity);

            await _repository.UpdateAsync(inventory);
        }

        public async Task ReleaseStockAsync(
        Guid productId,
        StockAdjustmentRequest request)
        {
            var inventory =
                await _repository.GetByProductIdAsync(productId);

            if (inventory is null)
                throw new NotFoundException();

            inventory.ReleaseStock(request.Quantity);

            await _repository.UpdateAsync(inventory);
        }

        public async Task DeleteAsync(Guid id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory is null)
                throw new NotFoundException();

            await _repository.DeleteAsync(inventory);
        }


        private static InventoryDto MapToDto(
      Inventory inventory)
        {
            return new InventoryDto(
                inventory.Id,
                inventory.ProductId,
                inventory.Quantity,
                inventory.ReservedQuantity,
                inventory.AvailableQuantity,
                inventory.LowStockThreshold,
                inventory.IsLowStock);
        }
    }
}
