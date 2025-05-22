using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace inventorybackend.Api.DTOs.Inventory
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("quantity")]
        public int QuantityInStock { get; set; }
        
        [JsonPropertyName("costPrice")]
        public decimal UnitPrice { get; set; }
        
        [JsonPropertyName("sellingPrice")]
        public decimal SellingPrice { get; set; }
        
        [JsonPropertyName("reorderPoint")]
        public int MinimumStockLevel { get; set; }
        
        [JsonPropertyName("supplierId")]
        public int SupplierId { get; set; }
        
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties for display
        public string SupplierName { get; set; }
        public string SupplierContact { get; set; }
        public string Category { get; set; }
    }
}