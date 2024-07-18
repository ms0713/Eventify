using Eventify.Modules.Events.Application.Abstractions.Messaging;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
