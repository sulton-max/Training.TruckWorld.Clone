using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Models;

public class ComponentDetails
{
    public Component Component { get; set; }
    public Guid? ContactDetailsId { get; set; }
    public ContactDetails? ContactDetails { get; set; }
}
