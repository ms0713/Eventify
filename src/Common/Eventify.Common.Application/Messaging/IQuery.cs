using Eventify.Common.Domain;
using MediatR;

namespace Eventify.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
