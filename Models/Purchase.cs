using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventorybackend.Api.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string Notes { get; set; }

        // Navigation properties
        public Supplier Supplier { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}