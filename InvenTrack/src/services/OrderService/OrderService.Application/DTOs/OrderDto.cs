using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.DTOs
{
    public record OrderDto(
     Guid Id,
     Guid UserId,
     string Status,
     decimal TotalAmount,
     DateTime CreatedAt,
     IEnumerable<OrderItemDto> Items);
}
