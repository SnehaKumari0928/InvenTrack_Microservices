using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.DTOs
{
    public record SupplierDto(
      Guid Id,
      string Name,
      string Email,
      string Phone,
      string Address);
}
