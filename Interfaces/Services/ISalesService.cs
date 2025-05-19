using inventorybackend.Api.DTOs.Sales;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface ISalesService
    {
        Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByDateDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate);
    }
} 