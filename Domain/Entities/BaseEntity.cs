
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        // a feild for domain event 
        private List<INotification> _domainEvents;
        [NotMapped]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
           
            _domainEvents.Clear();
        }
    }
}
