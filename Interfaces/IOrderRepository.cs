using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<Order>> GetByStatusAsync(string status);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByOrderNumberAsync(string orderNumber);
        Task UpdateOrderStatusAsync(int id, string status);
    }
} 