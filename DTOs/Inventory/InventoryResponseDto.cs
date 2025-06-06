namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryResponseDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReorderPoint { get; set; }
        public string Status { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}