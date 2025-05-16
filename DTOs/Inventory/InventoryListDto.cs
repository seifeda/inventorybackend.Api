namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryListDto
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public List<InventoryResponseDto> Items { get; set; }
    }
}