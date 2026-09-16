using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Requests
{
    public record StockAdjustmentRequest(
    int Quantity);
}
