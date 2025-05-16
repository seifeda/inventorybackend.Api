
using inventorybackend.Api.Repositoryies;
using inventorybackend.Api.Models; // Add this line or update with the correct namespace for InventoryItem

namespace inventorybackend.Api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }


        public async Task AddItemAsync(InventoryItem item)
        {
            item.CreatedAt = DateTime.UtcNow;
            await _repository.AddItemAsync(item);
        }
        public async Task UpdateItemAsync(InventoryItem item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateItemAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteItemAsync(id);
        }

        public async Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync(int threshold)
        {
            return await _repository.GetLowStockItemsAsync(threshold);
        }


    }
}

