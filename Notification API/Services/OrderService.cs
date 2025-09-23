using Microsoft.AspNetCore.SignalR;
using Notification_API.Hubs;
using Notification_API.Models;

namespace Notification_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();
        private int _nextId = 1;
        private readonly IHubContext<ProductHub> _hubContext;

        public OrderService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Order PlaceOrder(Order order)
        {
            order.OrderId = $"ORD{_nextId++:D6}";
            order.PlacedAt = DateTime.UtcNow;
            order.Status = "pending";

            _orders.Add(order);
            //_hubContext.Clients.All.SendAsync("ReceiveOrder", order);
            Console.WriteLine($"📢 Sending order {order.OrderId} to restaurant {order.RestaurantId}");
            // 🔔 Notify restaurant about new order
            _hubContext.Clients.User(order.RestaurantId)
                .SendAsync("ReceiveOrder", order);

            return order;
        }

        public Order? GetOrderById(string orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order? UpdateOrderStatus(string orderId, string status)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder == null) return null;

            existingOrder.Status = status;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            // 🔔 Notify customer about update (e.g., after payment)
            _hubContext.Clients.User(existingOrder.CustomerId)
                .SendAsync("ReceiveOrderStatus", existingOrder);

            return existingOrder;
        }

        public bool DeleteOrder(string orderId)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToRemove == null) return false;

            _orders.Remove(orderToRemove);
            return true;
        }
    }
}
