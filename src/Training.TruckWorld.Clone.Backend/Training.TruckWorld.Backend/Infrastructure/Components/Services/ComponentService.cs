using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services;

public class ComponentService : IComponentService
{
    private IDataContext _appDataContext;
    private IValidationService _validationService;

    public ComponentService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }

    public async ValueTask<Component> CreateAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(component);

        await _appDataContext.Components.AddAsync(component, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return component;
    }

    public ValueTask<ComponentFilterDataModel> GetFilterDataModel()
    {
        var dataModel = new ComponentFilterDataModel(
            _appDataContext.Components.Select(component => component.Category).Distinct().Select(category =>
            {
                return new KeyValuePair<string, ComponentCategory>(
                    $"{category.ToString()} ({_appDataContext.Components.Count(component => component.Category == category)})",
                    category);
            }),
            _appDataContext.Components.Select(component => component.ListingType).Distinct().Select(listingType =>
            {
                return new KeyValuePair<string, ListingType>(
                    $"{listingType} ({_appDataContext.Components.Count(component => component.ListingType == listingType)})",
                    listingType);
            }),
            _appDataContext.Components.Select(component => component.Manufacturer).Distinct().Select(manufacturer =>
            {
                return new KeyValuePair<string, string>(
                    $"{manufacturer} ({_appDataContext.Components.Count(component => component.Manufacturer == manufacturer)})",
                    manufacturer);
            }),
            _appDataContext.Components.Select(component => component.Contact.Location.City).Distinct().Select(state =>
            {
                return new KeyValuePair<string, string>(
                    $"{state} ({_appDataContext.Components.Count(component => component.Contact.Location.City == state)})",
                    state);
            }),
            _appDataContext.Components.Select(component => component.Condition).Distinct().Select(condition =>
            {
                return new KeyValuePair<string, ComponentCondition>(
                    $"{condition.ToString()} ({_appDataContext.Components.Count(component => component.Condition == condition)})",
                    condition);
            }),
            _appDataContext.Components.Select(component => component.Contact.Location.Country).Distinct().Select(
                country =>
                {
                    return new KeyValuePair<string, string>(
                        $"{country} ({_appDataContext.Components.Count(component => component.Contact.Location.Country == country)})",
                        country);
                })
        );

        return new ValueTask<ComponentFilterDataModel>(dataModel);
    }

    public ValueTask<ICollection<Component>> GetAsync(ComponentFilterModel filterModel)
    {
        return new ValueTask<ICollection<Component>>(_appDataContext.Components.Where(component =>
            (filterModel.Keyword is null ||
             (component.Manufacturer.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)
              || component.Model.Contains(filterModel.Keyword)))
            && (filterModel.ListingTypes == null || filterModel.ListingTypes.Contains(component.ListingType))
            && (filterModel.Categories == null || filterModel.Categories.Contains(component.Category))
            && (filterModel.MinYear == null || filterModel.MinYear <= component.Year)
            && (filterModel.MaxYear == null || filterModel.MaxYear >= component.Year)
            && (filterModel.MinDate == null || filterModel.MinDate <= component.CreatedDate)
            && (filterModel.MaxDate == null || filterModel.MaxDate >= component.CreatedDate)
            && (filterModel.MinPrice == null || filterModel.MinPrice <= component.Price)
            && (filterModel.MaxPrice == null || filterModel.MaxPrice >= component.Price)
        ).Skip((filterModel.PageToken - 1) * filterModel.PageSize).Take(filterModel.PageSize).ToList());
    }

    public ValueTask<Component> DeleteAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(component.Id, saveChanges, cancellationToken);
    }

    public async ValueTask<Component> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundComponent = await GetByIdAsync(id, cancellationToken)
                             ?? throw new EntityNotFoundException(typeof(Component));

        if (foundComponent.IsDeleted)
            throw new EntityDeletedException(typeof(Component), foundComponent.Id);

        await _appDataContext.Components.RemoveAsync(foundComponent, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return foundComponent;
    }

    public IQueryable<Component> Get(Expression<Func<Component, bool>> predicate)
        => _appDataContext.Components.Where(predicate.Compile()).AsQueryable();

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

    public async ValueTask<Component> UpdateAsync(Component component, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(component);

        var foundComponent =
            _appDataContext.Components.FirstOrDefault(searchingComponent => searchingComponent.Id == component.Id)
            ?? throw new EntityNotFoundException(typeof(Component));

        if (foundComponent is null)
            throw new EntityNotFoundException(typeof(Component), foundComponent.Id);

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

        await _appDataContext.Components.UpdateAsync(foundComponent, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return foundComponent;
    }

    private Component ToValidate(Component component)
    {
        if (!_validationService.IsValidComponentCategory(component.Category))
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid Category");
        if (!_validationService.IsValidDescription(component.Description))
        {
            Console.WriteLine(component.Description);
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid Description");
        }
        if (!_validationService.IsValidEmailAddress(component.Contact.EmailAddress))
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid EmailAddress");
        if (!_validationService.IsValidStuffs(component.Manufacturer))
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid Manufacturer");
        if (!_validationService.IsValidStuffs(component.Model))
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid Model");
        if (!_validationService.IsValidStuffs(component.SerialNumber))
            throw new InvalidEntityException(typeof(Component), component.Id, "Invalid SerialNumber");
        return component;
    }
}