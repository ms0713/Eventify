using System.Data.Common;
using Eventify.Modules.Events.Application.Abstractions.Data;
using Npgsql;

namespace Eventify.Modules.Events.Infrastructure.Data;
internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
