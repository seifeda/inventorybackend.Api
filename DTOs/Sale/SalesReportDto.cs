using System;
using System.Collections.Generic;
using System.Linq;

namespace inventorybackend.Api.DTOs.Sale
{
    public class SalesReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageTransactionValue { get; set; }
        public List<TopSellingItemDto> TopSellingItems { get; set; } = new();
        public List<SalesByPaymentMethodDto> SalesByPaymentMethod { get; set; } = new();
        public List<SalesByDayDto> SalesByDay { get; set; } = new();
    }

    public class TopSellingItemDto
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class SalesByPaymentMethodDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class SalesByDayDto
    {
        public DateTime Date { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}