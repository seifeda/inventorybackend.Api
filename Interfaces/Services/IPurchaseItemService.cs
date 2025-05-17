using inventorybackend.Api.DTOs.Purchase;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface IPurchaseItemService
    {
        Task<IEnumerable<PurchaseItemDto>> GetAllAsync();
        Task<PurchaseItemDto> GetByIdAsync(int id);
        Task<IEnumerable<PurchaseItemDto>> GetByPurchaseIdAsync(int purchaseId);
        Task<PurchaseItemDto> CreateAsync(CreatePurchaseItemDto createDto);
        Task<PurchaseItemDto> UpdateAsync(int id, UpdatePurchaseItemDto updateDto);
        Task DeleteAsync(int id);
    }
} 