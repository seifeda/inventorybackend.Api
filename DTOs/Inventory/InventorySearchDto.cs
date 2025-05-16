namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventorySearchDto
    {
        public string SearchTerm { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int? SupplierId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? LowStock { get; set; }
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}