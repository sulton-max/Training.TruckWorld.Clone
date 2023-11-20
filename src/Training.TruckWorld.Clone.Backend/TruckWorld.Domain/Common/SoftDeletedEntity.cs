namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents an entity with support for soft deletion and audit information.
/// </summary>
public class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}
