using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventorybackend.Api.Models
{
    public class PurchaseItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Purchase Purchase { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
