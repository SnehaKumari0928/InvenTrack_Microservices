using OrderService.Application.DTOs;
using OrderService.Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(
       CreateOrderRequest request);

        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDto>> GetByUserIdAsync(
            Guid userId);

        Task CancelAsync(Guid id);
    }
}
