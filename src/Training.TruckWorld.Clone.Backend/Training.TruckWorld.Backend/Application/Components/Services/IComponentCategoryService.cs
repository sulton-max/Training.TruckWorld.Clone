using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Components.Services;

public interface IComponentCategoryService
{
    IQueryable<ComponentCategory> Get(Expression<Func<ComponentCategory, bool>> predicate, CancellationToken cancellationToken = default);

    ValueTask<ICollection<ComponentCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ComponentCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ComponentCategory> CreateAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ComponentCategory> UpdateAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ComponentCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ComponentCategory> DeleteAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

}
