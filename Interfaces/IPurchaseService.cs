using inventorybackend.Api.DTOs.Purchase;

namespace inventorybackend.Api.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseDto>> GetAllAsync();
        Task<PurchaseDto> GetByIdAsync(int id);
        Task<PurchaseDto> CreateAsync(CreatePurchaseDto createDto);
        Task<PurchaseDto> UpdateAsync(int id, UpdatePurchaseDto updateDto);
        Task DeleteAsync(int id);
    }
} 