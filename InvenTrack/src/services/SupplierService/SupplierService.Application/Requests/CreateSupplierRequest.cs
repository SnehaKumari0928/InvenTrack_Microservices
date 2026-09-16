using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.Requests
{
    public record CreateSupplierRequest(
     string Name,
     string Email,
     string Phone,
     string Address);
}
