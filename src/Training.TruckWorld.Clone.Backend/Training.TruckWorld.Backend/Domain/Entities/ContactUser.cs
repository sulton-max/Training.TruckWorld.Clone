namespace Training.TruckWorld.Backend.Domain.Entities;

public class ContactUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Location Location { get; set; }

    public ContactUser() { }
    public ContactUser(string firstName, string lastName, string phoneNumber, string email, Location location)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Location = location;
    }
}
