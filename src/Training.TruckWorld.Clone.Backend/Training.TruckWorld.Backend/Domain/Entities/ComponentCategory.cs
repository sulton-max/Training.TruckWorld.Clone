using Training.TruckWorld.Backend.Domain.Common;
#pragma warning disable CS8618

namespace Training.TruckWorld.Backend.Domain.Entities;

public class ComponentCategory : SoftDeletedEntity
{
    public string Name { get; set; }
}
