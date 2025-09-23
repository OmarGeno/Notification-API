using Microsoft.AspNetCore.SignalR;

namespace Notification_API.Hubs
{
    public class ProductHub: Hub
    {
        public override Task OnConnectedAsync()
        {
            var claims = Context.User?.Claims?.ToList();
            Console.WriteLine($"🔑 Connection established. Claims count: {claims?.Count}");

            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    Console.WriteLine($"👉 {claim.Type} = {claim.Value}");
                }
            }

            var uid = Context.User?.FindFirst("user_id")?.Value;
            Console.WriteLine($"✅ Firebase UID: {uid}");

            return base.OnConnectedAsync();
        }
        //// You can send messages to all clients
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        ////By connection id
        //public async Task SendMessageToConnection(string connectionId, string message)
        //{
        //    await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        //}
        ////By user
        //public async Task SendMessageToUser(string userId, string message)
        //{
        //    await Clients.User(userId).SendAsync("ReceiveMessage", message);
        //}
        ////By group
        //public async Task SendMessageToGroup(string groupName, string message)
        //{
        //    await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        //}
    }
}
