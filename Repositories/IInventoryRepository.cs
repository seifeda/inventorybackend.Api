namespace inventorybackend.Api.Repositoryies
{
    using inventorybackend.Api.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItem>> GetAllAsync();
        Task<InventoryItem> GetItemByIdAsync(int id);
        Task AddItemAsync(InventoryItem item);
        Task UpdateItemAsync(InventoryItem item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<InventoryItem>> GetItemsBySupplierIdAsync(int supplierId);
        Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync(int threshold);

    }
}