using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
