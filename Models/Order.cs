namespace inventorybackend.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}