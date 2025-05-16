using AutoMapper;
using inventorybackend.Api.DTOs;
using inventorybackend.Api.Interfaces;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderNumber = Guid.NewGuid().ToString(),
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = dto.OrderItems.Select(i => new OrderItem
                {
                    InventoryItemId = i.InventoryItemId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            order.TotalAmount = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);

            var createdOrder = await _repository.CreateAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }
    }
}