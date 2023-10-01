using FileBaseContext.Abstractions.Models.Entity;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class ComponentCategory : SoftDeletedEntity
{
    public string Name { get; set; }
    
    public ComponentCategory() { }
    
    public ComponentCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}
