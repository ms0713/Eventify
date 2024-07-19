﻿using System.Data.Common;
using Dapper;
using Eventify.Common.Application.Data;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Events.Domain.TicketTypes;

namespace Eventify.Modules.Events.Application.TicketTypes.GetTicketType;

internal sealed class GetTicketTypeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketTypeQuery, TicketTypeResponse>
{
    public async Task<Result<TicketTypeResponse>> Handle(
        GetTicketTypeQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(TicketTypeResponse.Id)},
                 event_id AS {nameof(TicketTypeResponse.EventId)},
                 name AS {nameof(TicketTypeResponse.Name)},
                 price AS {nameof(TicketTypeResponse.Price)},
                 currency AS {nameof(TicketTypeResponse.Currency)},
                 quantity AS {nameof(TicketTypeResponse.Quantity)}
             FROM events.ticket_types
             WHERE id = @TicketTypeId
             """;

        TicketTypeResponse? ticketType =
            await connection.QuerySingleOrDefaultAsync<TicketTypeResponse>(sql, request);

        if (ticketType is null)
        {
            return Result.Failure<TicketTypeResponse>(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        return ticketType;
    }
}
