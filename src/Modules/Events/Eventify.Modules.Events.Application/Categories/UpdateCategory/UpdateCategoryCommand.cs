using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
