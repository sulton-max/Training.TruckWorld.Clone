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
    public async ValueTask<TruckCategory> CreateAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        
        await _appDataContext.TruckCategories.AddAsync(truckCategory);
      
        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }
        return truckCategory;
    }

    //Delete
    public async ValueTask<TruckCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var founded = _appDataContext.TruckCategories.FirstOrDefault(x => x.Id == id);
        if (founded is null)
            throw new EntityNotFoundException(typeof(TruckCategory), founded.Id);
        if (founded.IsDeleted)
            throw new EntityDeletedException(typeof(TruckCategory), founded.Id);

        founded.IsDeleted = true;
        founded.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
        {
            await _appDataContext.SaveChangesAsync();
        }
        return  founded;
    }

    //Delete
    public async ValueTask<TruckCategory> DeleteAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {

        var founded = _appDataContext.TruckCategories.FirstOrDefault(x => x.Id == truckCategory.Id);
        if (founded is null)
            throw new EntityNotFoundException(typeof(TruckCategory), founded.Id);
        if (founded.IsDeleted)
            throw new EntityDeletedException(typeof(TruckCategory), founded.Id);

        founded.IsDeleted = true;
        
        founded.DeletedDate = DateTime.UtcNow;
        
        if (saveChanges)
        {
           await _appDataContext.SaveChangesAsync();
        }
        return founded;

    }

    //Get
    public IQueryable<TruckCategory> Get(Expression<Func<TruckCategory, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _appDataContext.TruckCategories.Where(predicate.Compile()).AsQueryable();
    }

    public async ValueTask<ICollection<TruckCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
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
    public async ValueTask<TruckCategory> UpdateAsync(TruckCategory truckCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var founded = _appDataContext.TruckCategories.FirstOrDefault(x => x.Id == truckCategory.Id);

        if(founded is null)
        {
            throw new EntityNotFoundException(typeof(TruckCategory), founded.Id);
        }

        founded.Name = truckCategory.Name;
     
        founded.ModifiedDate = DateTime.UtcNow;

        if (saveChanges)
        {
           await _appDataContext.SaveChangesAsync();
        }
        return founded;
    }
}
