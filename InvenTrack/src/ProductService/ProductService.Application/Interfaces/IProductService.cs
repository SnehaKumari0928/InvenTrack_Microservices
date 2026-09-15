using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductRequest request);
        Task UpdateAsync(Guid id, UpdateProductRequest request);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto?> GetBySkuAsync(string sku);

        Task<ProductService.Application.Responses.PagedResult<ProductDto>> QueryAsync(ProductService.Application.Requests.ProductQueryRequest request);
    }
}
