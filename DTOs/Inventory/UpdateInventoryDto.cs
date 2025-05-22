using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace inventorybackend.Api.DTOs.Inventory
{
    public class UpdateInventoryDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [Required]
        [StringLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [StringLength(500)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0")]
        [JsonPropertyName("costPrice")]
        public decimal CostPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0")]
        [JsonPropertyName("sellingPrice")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        [JsonPropertyName("quantity")]
        public int QuantityInStock { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder point must be greater than or equal to 0")]
        [JsonPropertyName("reorderPoint")]
        public int ReorderPoint { get; set; }
        [Required]
        [JsonPropertyName("supplierId")]
        public int SupplierId { get; set; }
    }
}