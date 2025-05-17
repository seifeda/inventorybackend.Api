using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Inventory;
using inventorybackend.Api.Interfaces;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAll()
        {
            var inventoryItems = await _inventoryService.GetAllAsync();
            return Ok(inventoryItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDto>> GetById(int id)
        {
            try
            {
                var inventoryItem = await _inventoryService.GetByIdAsync(id);
                return Ok(inventoryItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<InventoryItemDto>> GetBySku(string sku)
        {
            try
            {
                var inventoryItem = await _inventoryService.GetBySkuAsync(sku);
                return Ok(inventoryItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetLowStockItems()
        {
            var lowStockItems = await _inventoryService.GetLowStockItemsAsync();
            return Ok(lowStockItems);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> Create(CreateInventoryItemDto createDto)
        {
            try
            {
                var inventoryItem = await _inventoryService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = inventoryItem.Id }, inventoryItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryItemDto>> Update(int id, UpdateInventoryItemDto updateDto)
        {
            try
            {
                var inventoryItem = await _inventoryService.UpdateAsync(id, updateDto);
                return Ok(inventoryItem);
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
                await _inventoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/stock")]
        public async Task<ActionResult> UpdateStockLevel(int id, [FromBody] int quantity)
        {
            try
            {
                await _inventoryService.UpdateStockLevelAsync(id, quantity);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
