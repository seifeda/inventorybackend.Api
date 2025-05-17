using AutoMapper;
using inventorybackend.Api.DTOs.Supplier;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> GetByEmailAsync(string email)
        {
            var supplier = await _supplierRepository.GetByEmailAsync(email);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with email {email} not found.");
            }
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetActiveSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetActiveSuppliersAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto createDto)
        {
            if (await _supplierRepository.ExistsByEmailAsync(createDto.Email))
            {
                throw new InvalidOperationException($"A supplier with email {createDto.Email} already exists.");
            }

            var supplier = _mapper.Map<Supplier>(createDto);
            var createdSupplier = await _supplierRepository.CreateAsync(supplier);
            return _mapper.Map<SupplierDto>(createdSupplier);
        }

        public async Task<SupplierDto> UpdateAsync(int id, UpdateSupplierDto updateDto)
        {
            var existingSupplier = await _supplierRepository.GetByIdAsync(id);
            if (existingSupplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }

            // Check if email is being changed and if it already exists
            if (existingSupplier.Email != updateDto.Email && 
                await _supplierRepository.ExistsByEmailAsync(updateDto.Email))
            {
                throw new InvalidOperationException($"A supplier with email {updateDto.Email} already exists.");
            }

            _mapper.Map(updateDto, existingSupplier);
            var updatedSupplier = await _supplierRepository.UpdateAsync(existingSupplier);
            return _mapper.Map<SupplierDto>(updatedSupplier);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _supplierRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }

            await _supplierRepository.DeleteAsync(id);
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            if (!await _supplierRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }

            await _supplierRepository.UpdateStatusAsync(id, isActive);
        }
    }
}
