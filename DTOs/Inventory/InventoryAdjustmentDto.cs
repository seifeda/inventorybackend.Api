namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryAdjustmentDto
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public string AdjustmentType { get; set; } // "Add" or "Remove"
        public string Reason { get; set; }
        public string Reference { get; set; }
    }
}