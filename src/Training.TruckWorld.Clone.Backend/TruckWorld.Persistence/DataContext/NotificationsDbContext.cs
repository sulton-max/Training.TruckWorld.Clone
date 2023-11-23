using Microsoft.EntityFrameworkCore;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.DataContext;

public class NotificationsDbContext : DbContext
{
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);
    }
}