namespace Notification_API.Models
{
    public class OrderItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }   // per unit price
        public decimal Total { get; set; }   // Quantity * Price
    }
}
