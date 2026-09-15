using System;

namespace ProductService.Application.DTOs
{
    public record ProductDto(Guid Id, string Name, string SKU, string Description, decimal Price, string Category);
}
