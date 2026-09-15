using System;

namespace ProductService.Application.Requests
{
    public record ProductQueryRequest(string? Search = null, string? Category = null, int PageNumber = 1, int PageSize = 20, string? SortBy = null);
}
