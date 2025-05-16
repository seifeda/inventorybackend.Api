namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryDetailDto : InventoryResponseDto
    {
        public decimal TotalValue { get; set; }
        public decimal ProfitMargin { get; set; }
        public int DaysUntilReorder { get; set; }
        public List<InventoryHistoryDto> History { get; set; }
    }

    public class InventoryHistoryDto
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public int Quantity { get; set; }
        public string Reference { get; set; }
        public string User { get; set; }
    }
}