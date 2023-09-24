using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class ComponentCategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ComponentCategory() { }
    public ComponentCategory(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
