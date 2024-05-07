using MediatR;
using SocialApp.data;
using SocialApp.Domain.Entities;

namespace SocialApp.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventAsync(this IMediator mediator,ApplicationDbContext context)
        {
            var domainEntites = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            var DomainEvents = domainEntites.SelectMany(x=>x.Entity.DomainEvents).ToList();
            domainEntites.ToList().ForEach(entity=>entity.Entity.ClearDomainEvents());
            foreach (var domainEvent in DomainEvents)
            {
                await mediator.Publish(domainEvent);

            }
        }
    }
}
