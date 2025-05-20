using inventorybackend.Api.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();
        Task<SaleDto?> GetSaleByIdAsync(int id);
        Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto);
        Task<bool> UpdateSaleAsync(UpdateSaleDto updateSaleDto);
        Task<bool> DeleteSaleAsync(int id);
        Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate);
    }
}