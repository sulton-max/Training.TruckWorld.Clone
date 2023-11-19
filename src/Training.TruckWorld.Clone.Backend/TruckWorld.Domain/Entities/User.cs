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
    public string Password { get; set; } = default!;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public User() {}

    /// <summary>
    /// Parameterized constructor to initialize a new instance of the class
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="emailAddress"></param>
    public User(string firstName,string lastName, string emailAddress)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }
}
