using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification_API.Hubs;
using Notification_API.Models;
using Notification_API.Services;

namespace Notification_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHubContext<ProductHub> _hubContext;
        public OrdersController(IOrderService orderService, IHubContext<ProductHub> hubContext)
        {
            _orderService = orderService;
            _hubContext = hubContext;
        }
        // 📌 POST api/orders/place
        [HttpPost("place")]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            if (order == null || order.Items.Count == 0)
            {

                return BadRequest("Invalid order data");
            }
            var newOrder = _orderService.PlaceOrder(order);
            return Ok(newOrder);
        }
        // 📌 GET api/orders/{orderId}
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderById(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null) return NotFound();

            return Ok(order);
        }

        // 📌 GET api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        // 📌 PUT api/orders/updateStatus
        [HttpPut("updateStatus")]
        public ActionResult<Order> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            var updatedOrder = _orderService.UpdateOrderStatus(request.OrderId, request.Status);
            if (updatedOrder == null) return NotFound("Order not found");

            return Ok(updatedOrder);
        }

        // 📌 DELETE api/orders/{orderId}
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(string orderId)
        {
            var result = _orderService.DeleteOrder(orderId);
            if (!result) return NotFound("Order not found");

            return NoContent();
        }
    }
}
