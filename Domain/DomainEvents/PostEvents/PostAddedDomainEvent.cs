using MediatR;
using SocialApp.Domain.Entities.Posts;
using SocialApp.Dtos.PostDtos;

namespace SocialApp.Domain.DomainEvents.PostEvents
{
    public record PostAddedDomainEvent(PostResponseDto dto) : INotification { }
    
}
