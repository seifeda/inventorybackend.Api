using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Inventory
{
    public class BaseInventoryDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "SKU cannot be longer than 50 characters")]
        public string Sku { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a positive number")]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0")]
        public decimal CostPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder point must be a positive number")]
        public int ReorderPoint { get; set; }

        [Required]
        public int SupplierId { get; set; }
    }
}
