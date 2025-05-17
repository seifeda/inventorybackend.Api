using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> GetByIdAsync(int id);
        Task<Supplier> GetByEmailAsync(string email);
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
        Task<Supplier> CreateAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task UpdateStatusAsync(int id, bool isActive);
    }
} 