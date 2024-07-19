using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
