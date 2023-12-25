using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Persistence.EntityConfigurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.Property(template => template.Subject).IsRequired().HasMaxLength(256);
    }
}
