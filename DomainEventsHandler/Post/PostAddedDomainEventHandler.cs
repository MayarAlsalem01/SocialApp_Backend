using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialApp.Domain.DomainEvents.PostEvents;
using SocialApp.Hubs;

namespace SocialApp.DomainEventsHandler.Post
{
    public class PostAddedDomainEventHandler : INotificationHandler<PostAddedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<PostNotificationHub>_hubContext;

        public PostAddedDomainEventHandler(
            ILogger<PostAddedDomainEventHandler> logger,
            IHubContext<PostNotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(PostAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            
            _logger.LogWarning($"a post created by {notification.dto.User.UserName} using {notification.dto.Id} id  ");
            await _hubContext.Clients.AllExcept(notification.dto.User.Id).SendAsync("RecieveNotfication",notification.dto,cancellationToken);
          
        }
    }
}
