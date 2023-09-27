namespace Training.TruckWorld.Backend.Domain.Entities;

public class Location
{
    public string Country { get; set; }
    public string City { get; set; }
    public string? Street { get; set; }


    public Location() { }
    public Location(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }
}
