namespace inventorybackend.Api.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string PurchaseNumber { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}