using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Purchase
{
    public class PurchaseItemDto
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        // Additional properties for display
        public string InventoryItemName { get; set; }
        public string InventoryItemSku { get; set; }
    }

    public class CreatePurchaseItemDto
    {
        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }

    public class UpdatePurchaseItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }
} 