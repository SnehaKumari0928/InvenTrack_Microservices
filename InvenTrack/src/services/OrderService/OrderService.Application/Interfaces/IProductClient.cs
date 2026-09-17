using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces
{
    public interface IProductClient
    {
        Task<ProductInfo?> GetProductAsync(Guid ProductId);
    }

    public record ProductInfo(Guid Id, string Name, decimal Price, Guid SupplierId);
}
