using FileBaseContext.Abstractions.Models.Entity;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class ComponentCategory : SoftDeletedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ComponentCategory() { }
    public ComponentCategory(int id, string name)
    {
        Id = id;
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}
