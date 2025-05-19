namespace inventorybackend.Api.DTOs.Sales
{
    public class SalesReportDto
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<TopSellingItemDto> TopSellingItems { get; set; } = new();
        public List<SalesByPaymentMethodDto> SalesByPaymentMethod { get; set; } = new();
        public List<SalesByDateDto> SalesByDate { get; set; } = new();
    }

    public class TopSellingItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class SalesByPaymentMethodDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class SalesByDateDto
    {
        public DateTime Date { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
    }
} 