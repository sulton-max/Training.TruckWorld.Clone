namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents entities id
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    Guid Id { get; set; }
}