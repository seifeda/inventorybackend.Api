using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace inventorybackend.Api.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .ToListAsync();
        }

        public async Task<InventoryItem> GetByIdAsync(int id)
        {
            var item = await _context.InventoryItems
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (item == null)
            {
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");
            }
            
            return item;
        }

        public async Task<InventoryItem> GetBySkuAsync(string sku)
        {
            var item = await _context.InventoryItems
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Sku == sku);
            
            if (item == null)
            {
                throw new KeyNotFoundException($"Inventory item with SKU {sku} not found.");
            }
            
            return item;
        }

        public async Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync()
        {
            return await _context.InventoryItems
                .Include(i => i.Supplier)
                .Where(i => i.QuantityInStock <= i.ReorderPoint)
                .ToListAsync();
        }

        public async Task<InventoryItem> CreateAsync(InventoryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await _context.InventoryItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventoryItem> UpdateAsync(InventoryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
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
            var item = await GetByIdAsync(id);
            item.QuantityInStock += quantity;
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}