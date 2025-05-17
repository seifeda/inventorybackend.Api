using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<Purchase> GetByIdAsync(int id);
        Task<Purchase> CreateAsync(Purchase purchase);
        Task<Purchase> UpdateAsync(Purchase purchase);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<decimal> CalculateTotalAmountAsync(int purchaseId);
    }
} 