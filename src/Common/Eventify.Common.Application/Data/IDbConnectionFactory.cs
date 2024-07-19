using System.Data.Common;

namespace Eventify.Common.Application.Data;
public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
