using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services;

public class ComponentService : IComponentService
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    public ComponentService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }
    public async ValueTask<Component> CreateAsync(Component component, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ToValidate(component);
        _appDataContext.Components.AddAsync(component, cancellationToken);
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return component;
    }

    public async ValueTask<Component> DeleteAsync(Component component, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComponent = await GetByIdAsync(component.Id, cancellationToken);
        if (foundComponent != null)
            throw new InvalidOperationException("Component not found");
        foundComponent.IsDeleted = true;
        foundComponent.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundComponent;
    }

    public async ValueTask<Component> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComponent = await GetByIdAsync(id, cancellationToken);
        if (foundComponent != null)
            throw new InvalidOperationException("Component not found");
        foundComponent.IsDeleted = true;
        foundComponent.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundComponent;
    }

    public IQueryable<Component> Get(Expression<Func<Component, bool>> predicate)
    {
        return _appDataContext.Components.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<ICollection<Component>> GetAsync(IEnumerable<Guid> ids)
    {
        var components = _appDataContext.Components.Where(component => ids.Contains(component.Id));
        return new ValueTask<ICollection<Component>>(components.ToList());
    }

    public ValueTask<Component?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var foundComponent = _appDataContext.Components.FirstOrDefault(component => component.Id == id);
        return new ValueTask<Component?>(foundComponent);
    }
    public async ValueTask<Component> UpdateAsync(Component component, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundComponent = _appDataContext.Components.FirstOrDefault(searchingComponent => searchingComponent.Id == component.Id);

        if (foundComponent is null)
            throw new InvalidOperationException("Truck not found");

        ToValidate(foundComponent);

        foundComponent.UserId = component.UserId;
        foundComponent.SerialNumber = component.SerialNumber;
        foundComponent.Manufacturer = component.Manufacturer;
        foundComponent.Model = component.Model;
        foundComponent.Category = component.Category;
        foundComponent.Year = component.Year;
        foundComponent.Condition = component.Condition;
        foundComponent.Description = component.Description;
        foundComponent.Price = component.Price;
        foundComponent.Quantity = component.Quantity;
        foundComponent.ListingType = component.ListingType;
        foundComponent.Weight = component.Weight;
        foundComponent.Price = component.Price;
        foundComponent.ModifiedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundComponent;
    }
    private Component ToValidate(Component component)
    {
        if (!_validationService.IsValidComponentCategory(component.Category))
            throw new Exception("Invalid Category");
        if (!_validationService.IsValidDescription(component.Description))
            throw new Exception("Invalid Description");
        if (!_validationService.IsValidEmailAddress(component.Contact.EmailAddress))
            throw new Exception("Invalid EmaildAddress");
        if (!_validationService.IsValidStuffs(component.Manufacturer))
            throw new Exception("Invalid Manufacturer");
        if (!_validationService.IsValidStuffs(component.Model))
            throw new Exception("Invalid Model");
        if (!_validationService.IsValidStuffs(component.SerialNumber)) 
            throw new Exception("Invalid SerialNumber");
        return component;
    }
}
