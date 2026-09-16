using SupplierService.Application.DTOs;
using SupplierService.Application.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierDto> CreateAsync(CreateSupplierRequest request);

        Task<IEnumerable<SupplierDto>> GetAllAsync();

        Task<SupplierDto> GetByIdAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateSupplierRequest request);

        Task DeleteAsync(Guid id);
    }
}
