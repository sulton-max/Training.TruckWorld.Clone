namespace Training.TruckWorld.Backend.Domain.Entities;
#pragma warning disable CS8618

public class ContactUser
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public Location Location { get; set; }

    public ContactUser()
    {
    }

    public ContactUser(Guid userId, string firstName, string lastName, string emailAddress, string phoneNumber,
        Location location)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Location = location;
    }
}