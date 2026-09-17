using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _db;

        public ProductRepository(ProductDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var list = await _db.Products.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> QueryAsync(string? search, string? category, int pageNumber, int pageSize, string? sortBy)
        {
            var query = _db.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(p => p.Name.Contains(s) || p.Description.Contains(s) || p.SKU.Contains(s));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                var c = category.Trim();
                query = query.Where(p => p.Category == c);
            }

            var total = await query.CountAsync();

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                // simple sorting by property name (only a few supported)
                switch (sortBy.ToLowerInvariant())
                {
                    case "price":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "-price":
                    case "price_desc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "name":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "-name":
                    case "name_desc":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    default:
                        // no-op: keep default ordering
                        break;
                }
            }

            // paging
            var skip = (Math.Max(pageNumber, 1) - 1) * Math.Max(pageSize, 1);
            var items = await query.Skip(skip).Take(Math.Max(pageSize, 1)).ToListAsync();

            return (items, total);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }


        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _db.Products.AsNoTracking().AnyAsync(p => p.SKU == sku);
        }
    }
}
