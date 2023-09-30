using System.Linq.Expressions;

using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services;

public class ComponentCategoryService : IComponentCategoryService
{
    private readonly IDataContext _appDataContext;

    public ComponentCategoryService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ComponentCategory> CreateAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _appDataContext.ComponentsCategories.AddAsync(componentCategory);

        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }
        return componentCategory;
    }


    public async ValueTask<ComponentCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComponent = await GetByIdAsync(id);

        if (foundComponent is null)
        {
            throw new InvalidOperationException("not found");
        }
        foundComponent.IsDeleted = true;

        foundComponent.DeletedDate = DateTime.UtcNow;

        await _appDataContext.SaveChangesAsync();

        return foundComponent;

    }

    public async ValueTask<ComponentCategory> DeleteAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComponent = await GetByIdAsync(componentCategory.Id);

        if (foundComponent == null)
        {
            throw new InvalidOperationException("not found");
        }

        await _appDataContext.SaveChangesAsync();

        return foundComponent;
    }

    public IQueryable<ComponentCategory> Get(Expression<Func<ComponentCategory, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _appDataContext.ComponentsCategories.Where(predicate.Compile()).AsQueryable();
    }

    public async ValueTask<ICollection<ComponentCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var componentCategory = _appDataContext.ComponentsCategories.Where(x => ids.Contains(x.Id));

        return await new ValueTask<ICollection<ComponentCategory>>(componentCategory.ToList());
    }

    public async ValueTask<ComponentCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var componentCategorys = _appDataContext.ComponentsCategories.FirstOrDefault(x => x.Id == id);

        return await new ValueTask<ComponentCategory?>(componentCategorys);
    }

    public async ValueTask<ComponentCategory> UpdateAsync(ComponentCategory componentCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var componentCategorys = _appDataContext.ComponentsCategories.FirstOrDefault(x => x.Id == componentCategory.Id);

        if (componentCategorys is null)
        {
            throw new InvalidOperationException("not found");
        }

        componentCategorys.ModifiedDate = DateTime.UtcNow;

        await _appDataContext.SaveChangesAsync();

        return componentCategorys;
    }
}
