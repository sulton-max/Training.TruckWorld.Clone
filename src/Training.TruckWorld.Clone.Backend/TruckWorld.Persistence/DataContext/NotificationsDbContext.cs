using Microsoft.EntityFrameworkCore;

using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.DataContext;

/// <summary>
/// Represents the database context for managing notifications-related entities
/// </summary>
public class NotificationsDbContext : DbContext
{
    /// <summary>
    /// Gets or Sets the DbSets for managing User entities
    /// </summary>
    public DbSet<User> Users => Set<User>();

    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);
    }
}