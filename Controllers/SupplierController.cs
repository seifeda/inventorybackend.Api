using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Supplier;
using inventorybackend.Api.Interfaces;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                return Ok(supplier);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<SupplierDto>> GetByEmail(string email)
        {
            try
            {
                var supplier = await _supplierService.GetByEmailAsync(email);
                return Ok(supplier);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetActiveSuppliers()
        {
            var suppliers = await _supplierService.GetActiveSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> Create(CreateSupplierDto createDto)
        {
            try
            {
                var supplier = await _supplierService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDto>> Update(int id, UpdateSupplierDto updateDto)
        {
            try
            {
                var supplier = await _supplierService.UpdateAsync(id, updateDto);
                return Ok(supplier);
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
                await _supplierService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] bool isActive)
        {
            try
            {
                await _supplierService.UpdateStatusAsync(id, isActive);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
