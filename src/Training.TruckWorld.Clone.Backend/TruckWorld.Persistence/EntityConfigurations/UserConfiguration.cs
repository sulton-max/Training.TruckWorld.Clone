using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(user => user.LastName).IsRequired().HasMaxLength(128);
        builder.Property(user => user.EmailAddress).IsRequired().HasMaxLength(128);
        builder.Property(user => user.PasswordHash).IsRequired().HasMaxLength(256);

        builder.HasIndex(user => user.EmailAddress).IsUnique();
    }
}
