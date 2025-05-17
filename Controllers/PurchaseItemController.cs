using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Purchase;
using inventorybackend.Api.Interfaces;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseItemController : ControllerBase
    {
        private readonly IPurchaseItemService _purchaseItemService;

        public PurchaseItemController(IPurchaseItemService purchaseItemService)
        {
            _purchaseItemService = purchaseItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseItemDto>>> GetAll()
        {
            var purchaseItems = await _purchaseItemService.GetAllAsync();
            return Ok(purchaseItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseItemDto>> GetById(int id)
        {
            try
            {
                var purchaseItem = await _purchaseItemService.GetByIdAsync(id);
                return Ok(purchaseItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("purchase/{purchaseId}")]
        public async Task<ActionResult<IEnumerable<PurchaseItemDto>>> GetByPurchaseId(int purchaseId)
        {
            var purchaseItems = await _purchaseItemService.GetByPurchaseIdAsync(purchaseId);
            return Ok(purchaseItems);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseItemDto>> Create(CreatePurchaseItemDto createDto)
        {
            var purchaseItem = await _purchaseItemService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = purchaseItem.Id }, purchaseItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PurchaseItemDto>> Update(int id, UpdatePurchaseItemDto updateDto)
        {
            try
            {
                var purchaseItem = await _purchaseItemService.UpdateAsync(id, updateDto);
                return Ok(purchaseItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _purchaseItemService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
} 