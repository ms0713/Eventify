using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Categories.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
