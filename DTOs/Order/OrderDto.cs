using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryItemId { get; set; }
        public string InventoryItemName { get; set; }
        public string InventoryItemSku { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderItemDto
    {
        [Required]
        public int InventoryItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }

    public class UpdateOrderDto
    {
        [Required]
        public string Status { get; set; }
    }
} 