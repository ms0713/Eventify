using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Ticketing.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
