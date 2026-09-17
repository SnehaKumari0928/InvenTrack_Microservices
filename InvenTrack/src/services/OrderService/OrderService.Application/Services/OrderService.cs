using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Application.Requests;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductClient _productClient;
        private readonly IInventoryClient _inventoryClient;
        private readonly ISupplierClient _supplierClient;

        public OrderService(
            IOrderRepository repository,
            IProductClient productClient,
            IInventoryClient inventoryClient,
            ISupplierClient supplierClient)
        {
            _repository = repository;
            _productClient = productClient;
            _inventoryClient = inventoryClient;
            _supplierClient = supplierClient;
        }


        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new ValidationException("UserId is required");
            }

            if (request.Items is null || !request.Items.Any())
            {
                throw new ValidationException("Order must contain atleast one item.");
            }

            var order = new Order(request.UserId);

            var reservedItems = new List<CreateOrderItemRequest>();

            try
            {
               foreach(var requestItem in request.Items)
                {
                    var product = await _productClient.GetProductAsync(requestItem.ProductId);

                    if(product is null)
                    {
                        throw new NotFoundException($"Product '{requestItem.ProductId}' was not found.");
                    }

                    if(product.SupplierId == Guid.Empty)
                    {
                        throw new ValidationException(
                         $"Product '{product.Id}' does not have a valid supplier.");
                    }

                    var supplier = await _supplierClient.GetSupplierAsync(product.SupplierId);

                    if(supplier is null)
                    {
                        throw new ValidationException(
                        $"Supplier '{product.SupplierId}' was not found.");
                    }

                    await _inventoryClient.ReserveStockAsync(requestItem.ProductId, requestItem.Quantity);

                    reservedItems.Add(requestItem);

                    order.AddItem(product.Id, requestItem.Quantity, product.Price);


                }

                await _repository.AddAsync(order);
                return MapToDto(order);
            }
            catch 
            {
                foreach(var item in reservedItems)
                {
                    try
                    {
                        await _inventoryClient.ReleaseStockAsync(item.ProductId, item.Quantity);
                    }
                    catch
                    {

                    }

                }
                throw;

            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order is null)
                throw new NotFoundException(
                    $"Order '{id}' was not found.");

            return MapToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(
      Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ValidationException(
                    "UserId is required.");

            var orders =
                await _repository.GetByUserIdAsync(userId);

            return orders.Select(MapToDto);
        }


        public async Task CancelAsync(Guid id)
        {
            var order =
                await _repository.GetByIdAsync(id);

            if (order is null)
                throw new NotFoundException(
                    $"Order '{id}' was not found.");

            order.Cancel();

            foreach (var item in order.Items)
            {
                await _inventoryClient.ReleaseStockAsync(
                    item.ProductId,
                    item.Quantity);
            }

            await _repository.UpdateAsync(order);
        }

        private static OrderDto MapToDto(Order order)
        {
            var items = order.Items.Select(item =>
                new OrderItemDto(
                    item.Id,
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.TotalPrice));

            return new OrderDto(
                order.Id,
                order.UserId,
                order.Status.ToString(),
                order.TotalAmount,
                order.CreatedAt,
                items);
        }

    }
}
