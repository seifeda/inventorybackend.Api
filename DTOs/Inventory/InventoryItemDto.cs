using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public int MinimumStockLevel { get; set; }
        public int SupplierId { get; set; }

        // Additional properties for display
        public string SupplierName { get; set; }
        public string Category { get; set; }
    }

    public class CreateInventoryItemDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Sku { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum stock level must be greater than or equal to 0")]
        public int MinimumStockLevel { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public string Category { get; set; }
    }

    public class UpdateInventoryItemDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Sku { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum stock level must be greater than or equal to 0")]
        public int MinimumStockLevel { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public string Category { get; set; }
    }
} 