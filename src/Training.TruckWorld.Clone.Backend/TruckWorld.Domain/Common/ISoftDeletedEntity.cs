namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents an entity that supports soft deletion with audit information.
/// </summary>
public interface ISoftDeletedEntity : IAuditableEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the date when the entity was deleted
    /// </summary>
    DateTime? DeletedDate { get; set; }
}
