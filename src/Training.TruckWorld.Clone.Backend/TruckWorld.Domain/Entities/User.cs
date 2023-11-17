using TruckWorld.Domain.Common;

namespace TruckWorld.Domain.Entities;
/// <summary>
/// Represents a user entity
/// </summary>
public class User : SoftDeletedEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;

    public User() {}

    public User(string firstName,string lastName, string emailAddress)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }
}
