using Eventify.Modules.Events.Application.Abstractions.Messaging;
using Eventify.Modules.Events.Application.Categories.GetCategory;

namespace Eventify.Modules.Events.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;
