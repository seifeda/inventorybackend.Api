using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Purchase;
using inventorybackend.Api.Interfaces.Services;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAll()
        {
            var purchases = await _purchaseService.GetAllAsync();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetById(int id)
        {
            try
            {
                var purchase = await _purchaseService.GetByIdAsync(id);
                return Ok(purchase);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> Create(CreatePurchaseDto createDto)
        {
            var purchase = await _purchaseService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, purchase);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PurchaseDto>> Update(int id, UpdatePurchaseDto updateDto)
        {
            try
            {
                var purchase = await _purchaseService.UpdateAsync(id, updateDto);
                return Ok(purchase);
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
                await _purchaseService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
