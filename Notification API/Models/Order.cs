namespace Notification_API.Models
{
    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string RestaurantId { get; set; } = string.Empty;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Address DeliveryAddress { get; set; } = new Address();

        public string Status { get; set; } = "pending";
        // could also be an enum if you want stricter typing

        public string PaymentMethod { get; set; } = "cash";
        // "cash", "card", "online"

        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
