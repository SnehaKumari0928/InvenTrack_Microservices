using Microsoft.EntityFrameworkCore;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Interfaces;
using SupplierService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Infrastructure.Repository
{
    public class SupplierRepository: ISupplierRepository
    {
        private readonly SupplierDbContext _context;

        public SupplierRepository(SupplierDbContext context)
        {
            _context = context;
        }


        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            return await _context.Suppliers
           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers
            .AsNoTracking()
            .ToListAsync();
        }

        

        public async Task<Supplier?> GetByEmailAsync(string email)
        {
            return await _context.Suppliers
            .FirstOrDefaultAsync(x => x.Email == email);
        }

        

        public async Task AddAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        

        public async Task DeleteAsync(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

    }
}
