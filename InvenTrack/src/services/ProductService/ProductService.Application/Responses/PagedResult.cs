using System.Collections.Generic;

namespace ProductService.Application.Responses
{
    public record PagedResult<T>(IEnumerable<T> Items, int TotalCount);
}
