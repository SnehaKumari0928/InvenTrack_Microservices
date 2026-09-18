using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Requests
{
    public record CreateOrderRequest(
      Guid UserId,
      IEnumerable<CreateOrderItemRequest> Items);
}
