using Shared.Common.Exceptions;
using SupplierService.Application.DTOs;
using SupplierService.Application.Interfaces;
using SupplierService.Application.Requests;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Application.Services
{
    public class SupplierService : ISupplierService
    {

        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierRequest request)
        {
            var existing = await _supplierRepository.GetByEmailAsync(request.Email);

            if (existing is not null)
            {
                throw new ValidationException(
                    $"A supplier with email '{request.Email}' already exists.");
            }

            var supplier = new Supplier(
                request.Name,
                request.Email,
                request.Phone,
                request.Address);

            await _supplierRepository.AddAsync(supplier);

            return MapToDto(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();

            return suppliers.Select(MapToDto);
        }

        public async Task<SupplierDto> GetByIdAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier is null)
                throw new NotFoundException();

            return MapToDto(supplier);
        }

        public async Task UpdateAsync(Guid id, UpdateSupplierRequest request)
        {
            var existing = await _supplierRepository.GetByIdAsync(id);

            if (existing is null)
                throw new NotFoundException();

            existing.Update(
                request.Name,
                request.Email,
                request.Phone,
                request.Address);

            await _supplierRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _supplierRepository.GetByIdAsync(id);

            if (existing is null)
                throw new NotFoundException();

            await _supplierRepository.DeleteAsync(existing);
        }


        private static SupplierDto MapToDto(Supplier supplier) =>
      new SupplierDto(
          supplier.Id,
          supplier.Name,
          supplier.Email,
          supplier.Phone,
          supplier.Address);
    }
}

