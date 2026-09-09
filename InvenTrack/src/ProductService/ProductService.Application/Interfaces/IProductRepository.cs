using ProductService.ProductService.Domain.Entities;

namespace ProductService.ProductService.Application.Interfaces
{
    public interface IProductRepository
    {

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product?> GetBySkuAsync(string sku);

        Task<List<Product>> GetAllAsync();

        Task AddAsync(Product product);

        void Update(Product product);

        void Delete(Product product);

        Task<bool> ExistsBySkuAsync(string sku);

        Task SaveChangesAsync();
    }
}
