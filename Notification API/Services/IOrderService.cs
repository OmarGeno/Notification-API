using Notification_API.Models;

namespace Notification_API.Services
{
    public interface IOrderService
    {
        Order PlaceOrder(Order order);
        Order? GetOrderById(string orderId);
        IEnumerable<Order> GetAllOrders();
        Order? UpdateOrderStatus(string orderId, string status);
        bool DeleteOrder(string orderId);
    }
}
