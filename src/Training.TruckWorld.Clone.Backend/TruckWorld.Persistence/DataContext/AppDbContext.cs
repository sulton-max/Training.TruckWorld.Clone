using Microsoft.EntityFrameworkCore;

namespace TruckWorld.Persistence.DataContext;

public class AppDbContext : DbContext
{
    /// <summary>
    /// DbContext created with necessary structure
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}