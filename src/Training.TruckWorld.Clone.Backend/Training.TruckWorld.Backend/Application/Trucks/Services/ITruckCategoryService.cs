using System.Linq.Expressions;

using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Trucks.Services;

public interface ITruckCategoryService
{
    IQueryable<TruckCategory> Get(Expression<Func<TruckCategory, bool>> predicate, CancellationToken cancellationToken = default);

    ValueTask<ICollection<TruckCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<TruckCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<TruckCategory> CreateAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TruckCategory> UpdateAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TruckCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<TruckCategory> DeleteAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

}
