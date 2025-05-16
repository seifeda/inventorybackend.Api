namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryStatisticsDto
    {
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<CategoryStatisticsDto> CategoryStatistics { get; set; }
        public List<StockLevelDto> StockLevels { get; set; }
    }

    public class CategoryStatisticsDto
    {
        public string Category { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class StockLevelDto
    {
        public string Status { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }
}