using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;
