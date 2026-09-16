using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
       
        Task<Product?> GetByIdAsync(Guid id);

        Task<Product?> GetBySkuAsync(string sku);

        Task<IEnumerable<Product>> GetAllAsync();

    
        Task<(IEnumerable<Product> Items, int TotalCount)> QueryAsync(string? search, string? category, int pageNumber, int pageSize, string? sortBy);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<bool> ExistsBySkuAsync(string sku);
    }
}
