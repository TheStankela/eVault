using Microsoft.AspNetCore.SignalR;

namespace eVault.Application.Hubs
{
    //NotificationHub
    public partial class BaseHub
    {
        public async Task SendTagNotification(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("tagNotificationResponse", Context.ConnectionId, message);
        }
    }
}
