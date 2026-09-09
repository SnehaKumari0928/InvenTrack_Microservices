using ProductService.ProductService.Application.DTOs;

namespace ProductService.ProductService.Application.Services
{
    public interface IProductService
    {

        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

        Task<ProductResponseDto?> GetByIdAsync(Guid id);

        Task<List<ProductResponseDto>> GetAllAsync();

        Task<bool> UpdateAsync(Guid id, UpdateProductDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}
