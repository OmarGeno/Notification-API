namespace Notification_API.Models
{
    public class UpdateOrderStatusRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "confirmed", "preparing", etc.
    }
}
