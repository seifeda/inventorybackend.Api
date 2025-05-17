using AutoMapper;
using inventorybackend.Api.DTOs.Order;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IInventoryRepository inventoryRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetByOrderNumberAsync(string orderNumber)
        {
            var order = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with number {orderNumber} not found.");
            }
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto createDto)
        {
            // Create order
            var order = new Order
            {
                UserId = createDto.UserId,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            // Process each order item
            foreach (var item in createDto.OrderItems)
            {
                var inventoryItem = await _inventoryRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem == null)
                {
                    throw new InvalidOperationException($"Inventory item with ID {item.InventoryItemId} not found.");
                }

                if (inventoryItem.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for item {inventoryItem.Name}. Available: {inventoryItem.Quantity}, Requested: {item.Quantity}");
                }

                var orderItem = new OrderItem
                {
                    InventoryItemId = item.InventoryItemId,
                    Quantity = item.Quantity,
                    UnitPrice = inventoryItem.SellingPrice
                };

                order.OrderItems.Add(orderItem);
                totalAmount += orderItem.TotalPrice;

                // Update inventory stock
                inventoryItem.Quantity -= item.Quantity;
                await _inventoryRepository.UpdateAsync(inventoryItem);
            }

            order.TotalAmount = totalAmount;
            var createdOrder = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateDto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Validate status transition
            if (!IsValidStatusTransition(order.Status, updateDto.Status))
            {
                throw new InvalidOperationException($"Cannot change order status from {order.Status} to {updateDto.Status}");
            }

            order.Status = updateDto.Status;
            var updatedOrder = await _orderRepository.UpdateAsync(order);
            return _mapper.Map<OrderDto>(updatedOrder);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _orderRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteAsync(id);
        }

        public async Task UpdateOrderStatusAsync(int id, string status)
        {
            if (!await _orderRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _orderRepository.UpdateOrderStatusAsync(id, status);
        }

        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            var validTransitions = new Dictionary<string, string[]>
            {
                { "Pending", new[] { "Processing", "Cancelled" } },
                { "Processing", new[] { "Shipped", "Cancelled" } },
                { "Shipped", new[] { "Delivered" } },
                { "Delivered", new string[] { } },
                { "Cancelled", new string[] { } }
            };

            return validTransitions.ContainsKey(currentStatus) &&
                   validTransitions[currentStatus].Contains(newStatus);
        }
    }
}