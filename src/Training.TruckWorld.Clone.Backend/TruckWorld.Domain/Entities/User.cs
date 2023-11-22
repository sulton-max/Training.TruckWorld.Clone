using TruckWorld.Domain.Common;

namespace TruckWorld.Domain.Entities;

/// <summary>
/// Represents a user entity
/// </summary>
public class User : SoftDeletedEntity
{
    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user 
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    ///Gets or sets the password of the user 
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Gets or sets the DateTimeOffset representing the expiry time.
    /// </summary>
    public DateTime ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating the activation status.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the verification link string.
    /// </summary>
    public string VerificationLink { get; set; } = string.Empty;
}
