using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseItems)
                    .ThenInclude(pi => pi.InventoryItem)
                .ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseItems)
                    .ThenInclude(pi => pi.InventoryItem)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (purchase == null)
                throw new InvalidOperationException($"Purchase with ID {id} not found.");
            return purchase;
        }

        public async Task<Purchase> CreateAsync(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> UpdateAsync(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task DeleteAsync(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseItems)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase != null)
            {
                // Remove all purchase items first
                _context.PurchaseItems.RemoveRange(purchase.PurchaseItems);
                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Purchases.AnyAsync(p => p.Id == id);
        }

        public async Task<decimal> CalculateTotalAmountAsync(int purchaseId)
        {
            return await _context.PurchaseItems
                .Where(pi => pi.PurchaseId == purchaseId)
                .SumAsync(pi => pi.TotalPrice);
        }
    }
}
