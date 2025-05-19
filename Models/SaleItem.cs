using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventorybackend.Api.Models
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        // Navigation properties
        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [ForeignKey("InventoryItemId")]
        public InventoryItem InventoryItem { get; set; }
    }
} 