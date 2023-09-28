namespace Training.TruckWorld.Backend.Domain.Common;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}
