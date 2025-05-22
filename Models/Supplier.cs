namespace inventorybackend.Api.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<InventoryItem> InventoryItems { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}