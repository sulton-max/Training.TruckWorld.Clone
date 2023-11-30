using Microsoft.EntityFrameworkCore;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.DataContext;

public class NotificationsDbContext : DbContext
{

    public DbSet<User> Users => Set<User>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

    public NotificationsDbContext(DbContextOptions options) : base(options)
    {
    }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);
    }
}