using inventorybackend.Api.Models;
using inventorybackend.Api.Data;
using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Interfaces;

namespace inventorybackend.Api.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers
                .Include(s => s.InventoryItems)
                .ToListAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _context.Suppliers
                .Include(s => s.InventoryItems)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetByEmailAsync(string email)
        {
            return await _context.Suppliers
                .Include(s => s.InventoryItems)
                .FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync()
        {
            return await _context.Suppliers
                .Include(s => s.InventoryItems)
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            supplier.IsActive = true;
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Suppliers.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Suppliers.AnyAsync(s => s.Email == email);
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                supplier.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
        }
    }
}
