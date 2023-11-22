using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckWorld.Domain.Entities;
using TruckWorld.Domain.Enums;

namespace TruckWorld.Persistence.EntityConfigurations;

/// <summary>
/// Configurations class for the User entity in the database.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity properties and their mappings.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.EmailAddress).HasMaxLength(64).IsRequired();
        builder.Property(user => user.PasswordHash).HasMaxLength(64).IsRequired();
    }
}
