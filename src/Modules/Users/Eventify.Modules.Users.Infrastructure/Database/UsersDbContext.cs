using Eventify.Common.Infrastructure.Inbox;
using Eventify.Common.Infrastructure.Outbox;
using Eventify.Modules.Users.Application.Abstractions.Data;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}
