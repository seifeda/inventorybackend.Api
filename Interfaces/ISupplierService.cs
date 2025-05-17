using inventorybackend.Api.DTOs.Supplier;

namespace inventorybackend.Api.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto> GetByIdAsync(int id);
        Task<SupplierDto> GetByEmailAsync(string email);
        Task<IEnumerable<SupplierDto>> GetActiveSuppliersAsync();
        Task<SupplierDto> CreateAsync(CreateSupplierDto createDto);
        Task<SupplierDto> UpdateAsync(int id, UpdateSupplierDto updateDto);
        Task DeleteAsync(int id);
        Task UpdateStatusAsync(int id, bool isActive);
    }
} 