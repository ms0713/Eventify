using Eventify.Common.Domain;

namespace Eventify.Modules.Events.Domain.Categories;
public sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
