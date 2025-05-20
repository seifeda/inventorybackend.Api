using inventorybackend.Api.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface ISalesService
    {
        Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate);
    }
}