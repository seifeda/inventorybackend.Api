using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Order;
using inventorybackend.Api.Interfaces.Services;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("number/{orderNumber}")]
        public async Task<ActionResult<OrderDto>> GetByOrderNumber(string orderNumber)
        {
            try
            {
                var order = await _orderService.GetByOrderNumberAsync(orderNumber);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByCustomerId(int customerId)
        {
            var orders = await _orderService.GetByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByStatus(string status)
        {
            var orders = await _orderService.GetByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto createDto)
        {
            try
            {
                var order = await _orderService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> Update(int id, UpdateOrderDto updateDto)
        {
            try
            {
                var order = await _orderService.UpdateAsync(id, updateDto);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(id, status);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}