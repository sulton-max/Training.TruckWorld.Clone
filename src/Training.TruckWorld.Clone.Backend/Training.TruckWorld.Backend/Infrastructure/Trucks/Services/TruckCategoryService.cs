using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Domain.Exceptions;

namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Services;

public class TruckCategoryService : ITruckCategoryService
{
    //DataContext
    private readonly IDataContext _appDataContext;

    public TruckCategoryService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    //Create
    public async ValueTask<TruckCategory> CreateAsync(TruckCategory truckCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await _appDataContext.TruckCategories.AddAsync(truckCategory, cancellationToken);

        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }

        return truckCategory;
    }

    //Delete
    public async ValueTask<TruckCategory> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var founded = _appDataContext.TruckCategories.FirstOrDefault(x => x.Id == id)
                      ?? throw new EntityNotFoundException(typeof(TruckCategory));

        if (founded.IsDeleted)
            throw new EntityDeletedException(typeof(TruckCategory), founded.Id);

        await _appDataContext.TruckCategories.RemoveAsync(founded, cancellationToken);

        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }

        return founded;
    }

    //Delete
    public ValueTask<TruckCategory> DeleteAsync(TruckCategory truckCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(truckCategory.Id);
    }

    //Get
    public IQueryable<TruckCategory> Get(Expression<Func<TruckCategory, bool>> predicate,
        CancellationToken cancellationToken = default)
        => _appDataContext.TruckCategories.Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ICollection<TruckCategory>> GetAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var truckCategory = _appDataContext.TruckCategories.Where(x => ids.Contains(x.Id));
        return await new ValueTask<ICollection<TruckCategory>>(truckCategory.ToList());
    }

    //GetByIdAsync
    public async ValueTask<TruckCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var truckCategory = _appDataContext.TruckCategories.FirstOrDefault(x => x?.Id == id);

        return await new ValueTask<TruckCategory?>(truckCategory);
    }

    //UpdateAsync
    public async ValueTask<TruckCategory> UpdateAsync(TruckCategory truckCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var founded = _appDataContext.TruckCategories.FirstOrDefault(x => x.Id == truckCategory.Id)
                      ?? throw new EntityNotFoundException(typeof(TruckCategory));


        founded.Name = truckCategory.Name;

        await _appDataContext.TruckCategories.UpdateAsync(founded, cancellationToken);

        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }

        return founded;
    }
}