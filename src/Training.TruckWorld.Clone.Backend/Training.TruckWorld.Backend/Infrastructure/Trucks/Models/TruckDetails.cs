using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Models;

public class TruckDetails
{
    public Truck Truck { get; set; }
    public Guid? ContactId { get; set; }
    public ContactDetails? ContactDetails { get; set; }
}