using inventorybackend.Api.DTOs.Sales;
using inventorybackend.Api.Interfaces.Services;

namespace inventorybackend.Api.Services
{
    public class SalesService : ISalesService
    {
        public async Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            // Mock data for now
            return new SalesReportDto
            {
                TotalSales = 150,
                TotalRevenue = 15000.00m,
                AverageOrderValue = 100.00m,
                TopSellingItems = new List<TopSellingItemDto>
                {
                    new() { ItemId = 1, ItemName = "Laptop", QuantitySold = 25, TotalRevenue = 5000.00m },
                    new() { ItemId = 2, ItemName = "Printer", QuantitySold = 15, TotalRevenue = 3000.00m },
                    new() { ItemId = 3, ItemName = "Monitor", QuantitySold = 20, TotalRevenue = 4000.00m }
                },
                SalesByPaymentMethod = new List<SalesByPaymentMethodDto>
                {
                    new() { PaymentMethod = "Credit Card", Count = 80, TotalAmount = 8000.00m },
                    new() { PaymentMethod = "Cash", Count = 40, TotalAmount = 4000.00m },
                    new() { PaymentMethod = "Bank Transfer", Count = 30, TotalAmount = 3000.00m }
                },
                SalesByDate = new List<SalesByDateDto>
                {
                    new() { Date = DateTime.Today.AddDays(-2), TotalSales = 50, TotalRevenue = 5000.00m },
                    new() { Date = DateTime.Today.AddDays(-1), TotalSales = 60, TotalRevenue = 6000.00m },
                    new() { Date = DateTime.Today, TotalSales = 40, TotalRevenue = 4000.00m }
                }
            };
        }

        public async Task<IEnumerable<SalesByDateDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Mock data for now
            return new List<SalesByDateDto>
            {
                new() { Date = startDate, TotalSales = 50, TotalRevenue = 5000.00m },
                new() { Date = startDate.AddDays(1), TotalSales = 60, TotalRevenue = 6000.00m },
                new() { Date = endDate, TotalSales = 40, TotalRevenue = 4000.00m }
            };
        }

        public async Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate)
        {
            // Mock data for now
            return new List<SalesByPaymentMethodDto>
            {
                new() { PaymentMethod = "Credit Card", Count = 80, TotalAmount = 8000.00m },
                new() { PaymentMethod = "Cash", Count = 40, TotalAmount = 4000.00m },
                new() { PaymentMethod = "Bank Transfer", Count = 30, TotalAmount = 3000.00m }
            };
        }
    }
} 