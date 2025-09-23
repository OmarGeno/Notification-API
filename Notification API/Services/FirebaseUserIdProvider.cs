using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Notification_API.Services
{
    public class FirebaseUserIdProvider: IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("user_id")?.Value
               ?? connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
