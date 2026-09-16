using SupplierService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Domain.Interfaces
{
    public interface ISupplierRepository
    {

        Task<Supplier?> GetByIdAsync(Guid id);

        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier?> GetByEmailAsync(string email);

        Task AddAsync(Supplier supplier);

        Task UpdateAsync(Supplier supplier);

        Task DeleteAsync(Supplier supplier);
    }
}
