namespace TruckWorld.Domain.Common;

/// <summary>
/// Represents entities id
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}