using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Persistence.EntityConfigurations;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.Property(template => template.Content).HasMaxLength(129_536);

        builder.HasIndex(template => new
        {
            template.Type,
            template.TemplateType
        })
            .IsUnique();

        builder.ToTable("NotificationTemplates")
            .HasDiscriminator(emailTemplate => emailTemplate.Type)
            .HasValue<EmailTemplate>(NotificationType.Email)
            .HasValue<SmsTemplate>(NotificationType.Sms);
    }
}
