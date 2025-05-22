using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Inventory;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Helpers;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<InventoryItemDto>>>> GetAll()
        {
            var items = await _inventoryService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<InventoryItemDto>> { Data = items });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<InventoryItemDto>>> GetById(int id)
        {
            var item = await _inventoryService.GetByIdAsync(id);
            return Ok(new ApiResponse<InventoryItemDto> { Data = item });
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<ApiResponse<InventoryItemDto>>> GetBySku(string sku)
        {
            try
            {
                var item = await _inventoryService.GetBySkuAsync(sku);
                return Ok(new ApiResponse<InventoryItemDto> { Data = item });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse<InventoryItemDto> { Success = false, Message = "Item not found" });
            }
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<ApiResponse<IEnumerable<InventoryItemDto>>>> GetLowStockItems()
        {
            var items = await _inventoryService.GetLowStockItemsAsync();
            return Ok(new ApiResponse<IEnumerable<InventoryItemDto>> { Data = items });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<InventoryItemDto>>> Create(CreateInventoryDto createDto)
        {
            var item = await _inventoryService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, new ApiResponse<InventoryItemDto> { Data = item });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<InventoryItemDto>>> Update(int id, UpdateInventoryDto updateDto)
        {
            var item = await _inventoryService.UpdateAsync(id, updateDto);
            return Ok(new ApiResponse<InventoryItemDto> { Data = item });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            await _inventoryService.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Success = true });
        }

        [HttpPatch("{id}/stock")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateStockLevel(int id, [FromBody] int quantity)
        {
            try
            {
                await _inventoryService.UpdateStockLevelAsync(id, quantity);
                return Ok(new ApiResponse<object> { Success = true });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponse<object> { Success = false, Message = "Item not found" });
            }
        }
    }
}
