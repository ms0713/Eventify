using Eventify.Common.Domain;

namespace Eventify.Common.Application.Exceptions;

public sealed class EventifyException : Exception
{
    public EventifyException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
