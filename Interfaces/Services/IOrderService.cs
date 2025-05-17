using inventorybackend.Api.DTOs.Order;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<OrderDto>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<OrderDto>> GetByStatusAsync(string status);
        Task<OrderDto> CreateAsync(CreateOrderDto createDto);
        Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateDto);
        Task DeleteAsync(int id);
        Task UpdateOrderStatusAsync(int id, string status);
    }
} 