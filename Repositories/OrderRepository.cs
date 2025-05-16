using Microsoft.EntityFrameworkCore;
using inventorybackend.Api.Data;
using inventorybackend.Api.Interfaces;
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

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders.Include(o => o.OrderItems).ToListAsync();

        public async Task<Order> GetByIdAsync(int id) =>
            await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
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
    }
}
