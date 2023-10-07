using Training.TruckWorld.Backend.Infrastructure.Components.Models;

namespace Training.TruckWorld.Backend.Application.Components.Services;

public interface IComponentManagementService
{
    ValueTask<ComponentDetails> CreateAsync(ComponentDetails componentDetails, Guid userId);
}