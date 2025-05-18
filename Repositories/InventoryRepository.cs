using inventorybackend.Api.Models;
using inventorybackend.Api.Data;
using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Interfaces.Repositories;

namespace inventorybackend.Api.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Existing method, mapped to interface
        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .ToListAsync();
        }

        // Interface method: GetAllItemsAsync
        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
        {
            return await GetAllAsync();
        }

        // Existing method, mapped to interface
        public async Task<InventoryItem?> GetByIdAsync(int id)
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Interface method: GetItemByIdAsync
        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
                throw new InvalidOperationException($"InventoryItem with id {id} not found.");
            return item;
        }

        // Existing method, mapped to interface
        public async Task<InventoryItem> CreateAsync(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Interface method: AddItemAsync
        public async Task AddItemAsync(InventoryItem item)
        {
            await CreateAsync(item);
        }

        // Interface method: UpdateItemAsync
        public async Task UpdateItemAsync(InventoryItem item)
        {
            var existingItem = await _context.InventoryItems.FindAsync(item.Id);
            if (existingItem == null)
                return;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        // Existing method, mapped to interface
        public async Task DeleteAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        // Interface method: DeleteItemAsync
        public async Task DeleteItemAsync(int id)
        {
            await DeleteAsync(id);
        }

        // Interface method: GetItemsBySupplierIdAsync
        public async Task<IEnumerable<InventoryItem>> GetItemsBySupplierIdAsync(int supplierId)
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .Where(i => i.SupplierId == supplierId)
                .ToListAsync();
        }

        // Existing method, mapped to interface
        public async Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .Where(i => i.Quantity <= i.ReorderPoint)
                .ToListAsync();
        }

        // Interface method: GetLowStockItemsAsync(int supplierId)
        public async Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync(int supplierId)
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .Where(i => i.Quantity <= i.ReorderPoint && i.SupplierId == supplierId)
                .ToListAsync();
        }

        public async Task<InventoryItem> GetBySkuAsync(string sku)
        {
            var item = await _context.InventoryItems
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Sku == sku);
            if (item == null)
                throw new InvalidOperationException($"InventoryItem with SKU {sku} not found.");
            return item;
        }

        public async Task<InventoryItem> UpdateAsync(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Update(inventoryItem);
            await _context.SaveChangesAsync();
            return inventoryItem;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.InventoryItems.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _context.InventoryItems.AnyAsync(i => i.Sku == sku);
        }

        public async Task UpdateStockLevelAsync(int id, int quantity)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem != null)
            {
                inventoryItem.QuantityInStock += quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}