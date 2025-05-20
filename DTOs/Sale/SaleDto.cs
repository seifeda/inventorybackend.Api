using System;
using System.Collections.Generic;

namespace inventorybackend.Api.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<SaleItemDto> SaleItems { get; set; } = new();
    }

    public class UpdateSaleDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public List<UpdateSaleItemDto>? SaleItems { get; set; }
    }

    public class SaleItemDto
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class UpdateSaleItemDto
    {
        public int? Id { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
