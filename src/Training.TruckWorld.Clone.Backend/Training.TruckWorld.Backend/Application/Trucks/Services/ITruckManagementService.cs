using Training.TruckWorld.Backend.Infrastructure.Trucks.Models;

namespace Training.TruckWorld.Backend.Application.Trucks.Services;

public interface ITruckManagementService
{
    ValueTask<TruckDetails> CreateAsync(TruckDetails truckDetails);
}
