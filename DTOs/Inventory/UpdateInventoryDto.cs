using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Inventory
{
    public class UpdateInventoryDto : BaseInventoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public new string SKU { get; set; }

        [Required]
        [StringLength(100)]
        public new string Name { get; set; }

        [StringLength(500)]
        public new string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0")]
        public new decimal CostPrice { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0")]
        public new decimal SellingPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder point must be greater than or equal to 0")]
        public new int ReorderPoint { get; set; }

        [Required]
        public new int SupplierId { get; set; }
    }
}
