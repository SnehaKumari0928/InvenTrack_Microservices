using Microsoft.EntityFrameworkCore;
using ProductService.ProductService.Application.Interfaces;
using ProductService.ProductService.Domain.Entities;
using ProductService.ProductService.Infrastructure.Persistence;

namespace ProductService.ProductService.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _context.Products
                .AnyAsync(p => p.SKU == sku);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
