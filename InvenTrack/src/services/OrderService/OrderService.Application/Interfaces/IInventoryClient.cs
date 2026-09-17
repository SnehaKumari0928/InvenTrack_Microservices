using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces
{
    public interface IInventoryClient
    {
       Task ReserveStockAsync(Guid productId, int quantity);
        
        Task ReleaseStockAsync(Guid productId, int quantity);
    }
}
