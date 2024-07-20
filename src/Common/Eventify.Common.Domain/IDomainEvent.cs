using MediatR;

namespace Eventify.Common.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}
