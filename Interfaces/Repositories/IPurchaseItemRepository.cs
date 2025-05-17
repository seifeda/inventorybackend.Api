using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces.Repositories
{
    public interface IPurchaseItemRepository
    {
        Task<IEnumerable<PurchaseItem>> GetAllAsync();
        Task<PurchaseItem> GetByIdAsync(int id);
        Task<IEnumerable<PurchaseItem>> GetByPurchaseIdAsync(int purchaseId);
        Task<PurchaseItem> CreateAsync(PurchaseItem purchaseItem);
        Task<PurchaseItem> UpdateAsync(PurchaseItem purchaseItem);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
} 