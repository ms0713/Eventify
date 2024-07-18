using System.Data.Common;

namespace Eventify.Modules.Events.Application.Abstractions.Data;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
