using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Purchase
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }

        // Additional properties for display
        public string SupplierName { get; set; }
        public ICollection<PurchaseItemDto> PurchaseItems { get; set; }
    }

    public class CreatePurchaseDto
    {
        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public ICollection<CreatePurchaseItemDto> PurchaseItems { get; set; }
    }

    public class UpdatePurchaseDto
    {
        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public string Notes { get; set; }
    }
} 