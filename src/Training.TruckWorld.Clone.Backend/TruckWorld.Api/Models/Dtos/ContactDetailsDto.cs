namespace TruckWorld.Api.Models.Dtos;

public class ContactDetailsDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Country { get; set; }
    
    public string State { get; set; }
    
    public string City { get; set; }
}