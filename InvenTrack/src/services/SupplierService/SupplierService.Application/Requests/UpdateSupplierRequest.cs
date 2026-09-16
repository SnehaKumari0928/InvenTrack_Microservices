using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.Requests
{
    public record UpdateSupplierRequest(
     Guid Id,
     string Name,
     string Email,
     string Phone,
     string Address);
}
