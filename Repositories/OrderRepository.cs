using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.InventoryItem)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.InventoryItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.InventoryItem)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.InventoryItem)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.InventoryItem)
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            // Generate order number
            order.OrderNumber = GenerateOrderNumber();
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<bool> ExistsByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        private string GenerateOrderNumber()
        {
            // Generate a unique order number based on timestamp
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 4)}";
        }
    }
}
