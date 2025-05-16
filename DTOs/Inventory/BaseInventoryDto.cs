namespace inventorybackend.Api.DTOs.Inventory
{
    public class BaseInventoryDto
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReorderPoint { get; set; }
        public int SupplierId { get; set; }
    }
}