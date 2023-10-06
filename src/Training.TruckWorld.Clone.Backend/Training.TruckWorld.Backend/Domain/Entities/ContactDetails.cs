using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;
#pragma warning disable CS8618

public class ContactDetails: SoftDeletedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}