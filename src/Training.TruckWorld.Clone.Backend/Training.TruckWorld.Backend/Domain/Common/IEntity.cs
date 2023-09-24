using FileBaseContext.Abstractions.Models.Entity;

namespace Training.TruckWorld.Backend.Domain.Common;

public interface IEntity : IFileSetEntity<Guid>
{
}
