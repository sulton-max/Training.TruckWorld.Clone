namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents an auditable entity that tracks creation and modification dates.
/// </summary>
public interface IAuditableEntity : IEntity
{
    /// <summary>
    /// Gets or sets the creation date of the entity.
    /// </summary>
    DateTime CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the last modification date of the entity.
    /// </summary>
    DateTime? ModifiedTime { get; set; }
}
