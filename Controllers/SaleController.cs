using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using inventorybackend.Api.DTOs.Sale;
using inventorybackend.Api.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventorybackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SaleController> _logger;

        public SaleController(ISaleService saleService, ILogger<SaleController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAllSales()
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sales");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(id);
                if (sale == null)
                {
                    return NotFound();
                }
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching sale with ID {id}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleDto createSaleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sale = await _saleService.CreateSaleAsync(createSaleDto);
                return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleDto updateSaleDto)
        {
            if (id != updateSaleDto.Id)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _saleService.UpdateSaleAsync(updateSaleDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating sale with ID {id}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                var result = await _saleService.DeleteSaleAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting sale with ID {id}");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<SalesReportDto>> GetSalesReport(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("Start date cannot be after end date");
                }

                var report = await _saleService.GetSalesReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating sales report");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("Start date cannot be after end date");
                }

                var sales = await _saleService.GetSalesByDateRangeAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sales by date range");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("by-payment-method")]
        public async Task<ActionResult<IEnumerable<SalesByPaymentMethodDto>>> GetSalesByPaymentMethod(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("Start date cannot be after end date");
                }

                var sales = await _saleService.GetSalesByPaymentMethodAsync(startDate, endDate);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sales by payment method");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}