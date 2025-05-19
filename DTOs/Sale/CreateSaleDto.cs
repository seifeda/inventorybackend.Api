using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string? Notes { get; set; }

        [Required]
        public List<CreateSaleItemDto> SaleItems { get; set; }
    }
} 