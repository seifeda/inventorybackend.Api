using System.Collections.Generic;
using System.Threading.Tasks;
using inventorybackend.Api.DTOs.Inventory;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItemDto>> GetAllAsync();
        Task<InventoryItemDto> GetByIdAsync(int id);
        Task<InventoryItemDto> CreateAsync(CreateInventoryDto createDto);
        Task<InventoryItemDto> UpdateAsync(int id, UpdateInventoryDto updateDto);
        Task DeleteAsync(int id);
        Task<InventoryItemDto> GetBySkuAsync(string sku);
        Task<IEnumerable<InventoryItemDto>> GetLowStockItemsAsync();
        Task UpdateStockLevelAsync(int id, int quantity);
    }
}