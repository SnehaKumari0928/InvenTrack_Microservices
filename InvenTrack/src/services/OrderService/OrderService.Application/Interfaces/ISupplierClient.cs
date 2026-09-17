using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Application.Interfaces
{
    public interface ISupplierClient
    {
        Task<SupplierInfo?> GetSupplierAsync(Guid supplierId);
    }

    public record SupplierInfo(Guid Id, string Name, string Email);
}
