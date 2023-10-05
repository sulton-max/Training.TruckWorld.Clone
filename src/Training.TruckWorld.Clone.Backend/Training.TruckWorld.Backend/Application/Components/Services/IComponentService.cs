using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Components.Services;

public interface IComponentService
{
    IQueryable<Component> Get(Expression<Func<Component, bool>> predicate);

    ValueTask<ICollection<Component>> GetAsync(IEnumerable<Guid> ids);

    ValueTask<ComponentFilterDataModel> GetFilterDataModel();

    ValueTask<ICollection<Component>> GetAsync(ComponentFilterModel filterModel);

    ValueTask<Component?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Component> CreateAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Component> UpdateAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Component> DeleteAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Component> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}