using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventorybackend.Api.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string? Notes { get; set; }

        [Required]
        public string Status { get; set; } // Pending, Completed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<SaleItem> SaleItems { get; set; }
    }
} 