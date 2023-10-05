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
}