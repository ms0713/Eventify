using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
