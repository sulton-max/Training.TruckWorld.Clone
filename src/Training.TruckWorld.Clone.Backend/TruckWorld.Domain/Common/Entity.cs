namespace TruckWorld.Domain.Common;

/// <summary>
/// Base class for entities with a unique identifier.
/// </summary>
public class Entity : IEntity
{
    public Guid Id { get; set; }
}
