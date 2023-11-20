namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents an entity with audit information.
/// </summary>
public class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
