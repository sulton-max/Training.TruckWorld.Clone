using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Trucks.Services;

public interface ITruckService
{
    IQueryable<Truck> Get(Expression<Func<Truck, bool>> predicate);

    ValueTask<ICollection<Truck>> GetAsync(IEnumerable<Guid> ids);

    ValueTask<Truck?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    ValueTask<Truck> CreateAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Truck> UpdateAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Truck> DeleteAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Truck> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
