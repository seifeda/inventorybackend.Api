using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Repositories
{
    public class PurchaseItemRepository : IPurchaseItemRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseItem>> GetAllAsync()
        {
            return await _context.PurchaseItems
                .Include(pi => pi.InventoryItem)
                .Include(pi => pi.Purchase)
                .ToListAsync();
        }

        public async Task<PurchaseItem> GetByIdAsync(int id)
        {
            return await _context.PurchaseItems
                .Include(pi => pi.InventoryItem)
                .Include(pi => pi.Purchase)
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<IEnumerable<PurchaseItem>> GetByPurchaseIdAsync(int purchaseId)
        {
            return await _context.PurchaseItems
                .Include(pi => pi.InventoryItem)
                .Where(pi => pi.PurchaseId == purchaseId)
                .ToListAsync();
        }

        public async Task<PurchaseItem> CreateAsync(PurchaseItem purchaseItem)
        {
            purchaseItem.TotalPrice = purchaseItem.Quantity * purchaseItem.UnitPrice;
            _context.PurchaseItems.Add(purchaseItem);
            await _context.SaveChangesAsync();
            return purchaseItem;
        }

        public async Task<PurchaseItem> UpdateAsync(PurchaseItem purchaseItem)
        {
            purchaseItem.TotalPrice = purchaseItem.Quantity * purchaseItem.UnitPrice;
            _context.PurchaseItems.Update(purchaseItem);
            await _context.SaveChangesAsync();
            return purchaseItem;
        }

        public async Task DeleteAsync(int id)
        {
            var purchaseItem = await _context.PurchaseItems.FindAsync(id);
            if (purchaseItem != null)
            {
                _context.PurchaseItems.Remove(purchaseItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PurchaseItems.AnyAsync(pi => pi.Id == id);
        }
    }
} 