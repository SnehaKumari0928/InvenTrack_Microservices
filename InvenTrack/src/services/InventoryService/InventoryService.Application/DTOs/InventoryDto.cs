using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.DTOs
{
    public record InventoryDto(
    Guid Id,
    Guid ProductId,
    int Quantity,
    int ReservedQuantity,
    int AvailableQuantity,
    int LowStockThreshold,
    bool IsLowStock);
}
