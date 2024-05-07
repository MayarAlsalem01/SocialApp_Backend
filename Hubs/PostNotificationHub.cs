using Microsoft.AspNetCore.SignalR;

namespace SocialApp.Hubs
{
    public class PostNotificationHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            var userId = Context.User?.Identity?.Name?.ToString();
           await Clients.All.SendAsync("ReceiveNotfication",userId,message);
           
        }
    }
}
