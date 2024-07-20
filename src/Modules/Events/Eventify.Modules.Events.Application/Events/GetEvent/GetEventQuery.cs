using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
