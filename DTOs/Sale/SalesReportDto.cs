namespace inventorybackend.Api.DTOs.Sale
{
    public class SalesReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageTransactionValue { get; set; }
        public List<SalesByPaymentMethodDto> SalesByPaymentMethod { get; set; }
        public List<SalesByDayDto> SalesByDay { get; set; }
        public List<TopSellingItemDto> TopSellingItems { get; set; }
    }

    public class SalesByPaymentMethodDto
    {
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
    }

    public class SalesByDayDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
    }

    public class TopSellingItemDto
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
} 