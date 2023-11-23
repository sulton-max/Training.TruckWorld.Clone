namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents an entity with audit information.
/// </summary>
public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedTime { get; set; }
    public DateTime? ModifiedTime { get; set; }
}
