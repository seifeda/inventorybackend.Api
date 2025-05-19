using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.Services;
using inventorybackend.Api.DTOs.Sales;
using inventorybackend.Api.Interfaces.Services;

namespace inventorybackend.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetSalesReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var report = await _salesService.GetSalesReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetSalesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sales = await _salesService.GetSalesByDateRangeAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("payment-method")]
        public async Task<IActionResult> GetSalesByPaymentMethod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sales = await _salesService.GetSalesByPaymentMethodAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 