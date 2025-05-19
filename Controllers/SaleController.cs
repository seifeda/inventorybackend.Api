using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Sale;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(id);
                return Ok(sale);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale(CreateSaleDto createSaleDto)
        {
            try
            {
                var sale = await _saleService.CreateSaleAsync(createSaleDto);
                return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Sale>> UpdateSale(int id, CreateSaleDto updateSaleDto)
        {
            try
            {
                var sale = await _saleService.UpdateSaleAsync(id, updateSaleDto);
                return Ok(sale);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(int id)
        {
            var result = await _saleService.DeleteSaleAsync(id);
            if (!result)
                return NotFound($"Sale with ID {id} not found.");

            return NoContent();
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var sales = await _saleService.GetSalesByDateRangeAsync(startDate, endDate);
            return Ok(sales);
        }

        [HttpGet("customer/{customerName}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesByCustomerName(string customerName)
        {
            var sales = await _saleService.GetSalesByCustomerNameAsync(customerName);
            return Ok(sales);
        }

        [HttpGet("total-sales")]
        public async Task<ActionResult<decimal>> GetTotalSalesByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var total = await _saleService.GetTotalSalesByDateRangeAsync(startDate, endDate);
            return Ok(total);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateSaleStatus(int id, [FromBody] string status)
        {
            var result = await _saleService.UpdateSaleStatusAsync(id, status);
            if (!result)
                return NotFound($"Sale with ID {id} not found.");

            return NoContent();
        }

        [HttpGet("report")]
        public async Task<ActionResult<SalesReportDto>> GetSalesReport(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var report = await _saleService.GenerateSalesReportAsync(startDate, endDate);
            return Ok(report);
        }

        [HttpGet("report/payment-methods")]
        public async Task<ActionResult<IEnumerable<SalesByPaymentMethodDto>>> GetSalesByPaymentMethod(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var report = await _saleService.GetSalesByPaymentMethodAsync(startDate, endDate);
            return Ok(report);
        }

        [HttpGet("report/daily")]
        public async Task<ActionResult<IEnumerable<SalesByDayDto>>> GetSalesByDay(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var report = await _saleService.GetSalesByDayAsync(startDate, endDate);
            return Ok(report);
        }

        [HttpGet("report/top-items")]
        public async Task<ActionResult<IEnumerable<TopSellingItemDto>>> GetTopSellingItems(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int limit = 10)
        {
            var report = await _saleService.GetTopSellingItemsAsync(startDate, endDate, limit);
            return Ok(report);
        }
    }
} 