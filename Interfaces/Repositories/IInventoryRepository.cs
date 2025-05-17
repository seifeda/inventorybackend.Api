using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItem>> GetAllAsync();
        Task<InventoryItem> GetByIdAsync(int id);
        Task<InventoryItem> GetBySkuAsync(string sku);
        Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync();
        Task<InventoryItem> CreateAsync(InventoryItem inventoryItem);
        Task<InventoryItem> UpdateAsync(InventoryItem inventoryItem);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsBySkuAsync(string sku);
        Task UpdateStockLevelAsync(int id, int quantity);
    }
} 