using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;
#pragma warning disable CS8618

public class User : SoftDeletedEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string EmailAddress { get; set; }
    

    public User() { }

    public User(string firstName, string lastName, string emailAddress)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        CreatedDate = DateTime.UtcNow;
    }
}
