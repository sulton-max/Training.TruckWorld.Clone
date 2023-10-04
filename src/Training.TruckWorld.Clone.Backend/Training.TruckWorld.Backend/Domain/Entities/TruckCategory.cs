using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;
#pragma warning disable CS8618

public class TruckCategory : SoftDeletedEntity
{
    public string Name { get; set; }
}

