using inventorybackend.Api.DTOs.Sale;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task<Sale> CreateSaleAsync(CreateSaleDto createSaleDto);
        Task<Sale> UpdateSaleAsync(int id, CreateSaleDto updateSaleDto);
        Task<bool> DeleteSaleAsync(int id);
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Sale>> GetSalesByCustomerNameAsync(string customerName);
        Task<decimal> GetTotalSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> UpdateSaleStatusAsync(int id, string status);
        
        // New reporting methods
        Task<SalesReportDto> GenerateSalesReportAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SalesByDayDto>> GetSalesByDayAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TopSellingItemDto>> GetTopSellingItemsAsync(DateTime startDate, DateTime endDate, int limit = 10);
    }
} 