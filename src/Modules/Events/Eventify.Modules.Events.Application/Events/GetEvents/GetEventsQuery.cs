using Eventify.Modules.Events.Application.Abstractions.Messaging;

namespace Eventify.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<EventResponse>>;
