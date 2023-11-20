using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TruckWorld.Domain.Entities;

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
        builder.HasKey(userId => userId.Id);

        builder.Property(email => email.EmailAddress).HasMaxLength(100).IsRequired();
        builder.Property(password => password.Password).HasMaxLength(100).IsRequired();
    }
}
