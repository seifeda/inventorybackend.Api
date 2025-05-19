using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.InventoryItem)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.InventoryItem)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sale> CreateAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale)
        {
            _context.Entry(sale).State = EntityState.Modified;
            sale.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.InventoryItem)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetByCustomerNameAsync(string customerName)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.InventoryItem)
                .Where(s => s.CustomerName.Contains(customerName))
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .SumAsync(s => s.TotalAmount);
        }
    }
} 