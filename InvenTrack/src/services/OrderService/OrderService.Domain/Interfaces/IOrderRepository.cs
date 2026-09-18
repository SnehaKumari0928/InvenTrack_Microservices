using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);

    Task AddAsync(Order order);

    Task UpdateAsync(Order order);

    Task DeleteAsync(Order order);
}