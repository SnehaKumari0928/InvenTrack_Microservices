using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.DTOs
{
    public record OrderItemDto(
     Guid Id,
     Guid ProductId,
     int Quantity,
     decimal UnitPrice,
     decimal TotalPrice);
}
