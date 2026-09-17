using OrderService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Requests
{
    public record CreateOrderItemRequest(
    Guid ProductId,
    int Quantity);
}
