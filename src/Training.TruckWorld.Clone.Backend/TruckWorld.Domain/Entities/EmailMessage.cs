using TruckWorld.Domain.Common.Entities;

namespace TruckWorld.Domain.Entities;

public class EmailMessage : IEntity
{
    public Guid Id { get; set; }
}