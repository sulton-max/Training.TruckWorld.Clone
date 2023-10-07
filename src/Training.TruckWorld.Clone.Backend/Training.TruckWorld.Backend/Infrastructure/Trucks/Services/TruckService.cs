using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Domain.Exceptions;


namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Services;

public class TruckService : ITruckService
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    private readonly IContactService _contactService;
    private ITruckCategoryService _categoryService;

    public TruckService(IDataContext appDataContext, IValidationService validationService, IContactService contactService, ITruckCategoryService truckCategoryService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
        _contactService = contactService;
        _categoryService = truckCategoryService;
    }

    public async ValueTask<Truck> CreateAsync(Truck truck, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(truck);

        await _appDataContext.Trucks.AddAsync(truck, cancellationToken);

        if (saveChanges)
            await _appDataContext.Trucks.SaveChangesAsync();

        return truck;
    }

    public ValueTask<Truck> DeleteAsync(Truck truck, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(truck.Id, saveChanges, cancellationToken);
    }

    public async ValueTask<Truck> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundTruck = await GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException(typeof(Truck));

        if (foundTruck.IsDeleted)
            throw new EntityDeletedException(typeof(Truck), foundTruck.Id);

        await _appDataContext.Trucks.RemoveAsync(foundTruck, cancellationToken);

        if (saveChanges)
            await _appDataContext.Trucks.SaveChangesAsync();

        return foundTruck;
    }

    public IQueryable<Truck> Get(Expression<Func<Truck, bool>> predicate)
    {
        return _appDataContext.Trucks.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<TruckFilterDataModel> GetFilterDataModel()
    {
        var dataModel = new TruckFilterDataModel(
            _appDataContext.Trucks.Select(truck => truck.ListingType).Distinct().Select(listingType =>
            {
                return new KeyValuePair<string, ListingType>(
                    $"{listingType.ToString()} ({_appDataContext.Trucks.Count(truck => truck.ListingType == listingType)})",
                    listingType);
            }),
            _appDataContext.TruckCategories.Distinct().Select(category =>
            {
                return new KeyValuePair<string, string>(
                    $"{category.ToString()} ({_appDataContext.Trucks.Count(truck => truck.CategoryId == category.Id)})",
                    category.Name);
            }),
            _appDataContext.Trucks.Select(truck => truck.Manufacturer).Distinct().Select(manufacturer =>
            {
                return new KeyValuePair<string, string>(
                    $"{manufacturer} ({_appDataContext.Trucks.Count(truck => truck.Manufacturer == manufacturer)})",
                    manufacturer);
            }),
            _appDataContext.Contacts.Select(contact => contact.City).Distinct().Select(state =>
            {
                return new KeyValuePair<string, string>(
                    $"{state} ({_appDataContext.Trucks.Count(truck => GetContactDetails(truck).City == state)})",
                    state);
            }),
            _appDataContext.Trucks.Select(truck => truck.Condition).Distinct().Select(condition =>
            {
                return new KeyValuePair<string, TruckCondition>(
                    $"{condition.ToString()} ({_appDataContext.Trucks.Count(truck => truck.Condition == condition)})",
                    condition);
            }),
            _appDataContext.Contacts.Select(contact => contact.Country).Distinct().Select(country =>
            {
                return new KeyValuePair<string, string>(
                    $"{country} ({_appDataContext.Trucks.Count(truck => GetContactDetails(truck).Country == country)})",
                    country);
            })
        );

        return new ValueTask<TruckFilterDataModel>(dataModel);
    }

    public ValueTask<ICollection<Truck>> GetAsync(TruckFilterModel filterModel = null)
    {
        return new ValueTask<ICollection<Truck>>(_appDataContext.Trucks.Where(truck => (filterModel is null) ||
                (filterModel.Keyword == null ||
                 (truck.Manufacturer.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)
                  || truck.Model.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)))
                && (filterModel.ListingTypes == null || filterModel.ListingTypes.Contains(truck.ListingType))
                && (filterModel.Categories == null ||
                    filterModel.Categories.Contains(_categoryService.GetByIdAsync(truck.CategoryId).Result.Name))
                && (!filterModel.MinYear.HasValue || filterModel.MinYear <= truck.Year)
                && (!filterModel.MaxYear.HasValue || filterModel.MaxYear >= truck.Year)
                && (!filterModel.MinOdometer.HasValue || filterModel.MinOdometer <= truck.Odometer)
                && (!filterModel.MaxOdometer.HasValue || filterModel.MaxOdometer <= truck.Odometer)
                && (!filterModel.MinPrice.HasValue || filterModel.MinPrice <= truck.Price)
                && (!filterModel.MaxPrice.HasValue || filterModel.MaxPrice >= truck.Price)
                && (!filterModel.MinDate.HasValue || filterModel.MinDate <= truck.CreatedDate)
                && (!filterModel.MaxDate.HasValue || filterModel.MaxDate >= truck.CreatedDate)
                && (filterModel.State == null) || filterModel.State.Equals(GetContactDetails(truck).City)
                && (filterModel.Country == null) || filterModel.Country.Equals(GetContactDetails(truck).Country))
            .Skip((filterModel.PageToken - 1) * filterModel.PageSize).Take(filterModel.PageSize).ToArray());
    }

    public ValueTask<ICollection<Truck>> GetAsync(IEnumerable<Guid> ids)
    {
        var trucks = _appDataContext.Trucks.Where(truck => ids.Contains(truck.Id));
        return new ValueTask<ICollection<Truck>>(trucks.ToList());
    }

    public ValueTask<Truck?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var foundTruck = _appDataContext.Trucks.FirstOrDefault(truck => truck.Id == id);
        return new ValueTask<Truck?>(foundTruck);
    }

    public async ValueTask<Truck> UpdateAsync(Truck truck, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundTruck = _appDataContext.Trucks.FirstOrDefault(searchingTruck => searchingTruck.Id == truck.Id)
                         ?? throw new EntityNotFoundException(typeof(Truck));

        ToValidate(truck);

        foundTruck.UserId = truck.UserId;
        foundTruck.SerialNumber = truck.SerialNumber;
        foundTruck.Manufacturer = truck.Manufacturer;
        foundTruck.Model = truck.Model;
        foundTruck.CategoryId = truck.CategoryId;
        foundTruck.Year = truck.Year;
        foundTruck.Condition = truck.Condition;
        foundTruck.Description = truck.Description;
        foundTruck.Price = truck.Price;
        foundTruck.Odometer = truck.Odometer;
        foundTruck.ListingType = truck.ListingType;
        foundTruck.EngineType = truck.EngineType;
        foundTruck.FuelType = truck.FuelType;
        foundTruck.Color = truck.Color;
        foundTruck.ContactId = truck.ContactId;

        await _appDataContext.Trucks.UpdateAsync(foundTruck, cancellationToken);

        if (saveChanges)
            await _appDataContext.Trucks.SaveChangesAsync();

        return foundTruck;
    }

    private ContactDetails GetContactDetails(Truck truck)
        => _contactService.Get(contact => contact.Id == truck.ContactId).FirstOrDefault() ??
           throw new EntityNotFoundException(typeof(ContactDetails));

    private Truck ToValidate(Truck truck)
    {
        if (!_categoryService.Get(category => category.Id == truck.CategoryId).Any())
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Category");
        if (!_validationService.IsValidDescription(truck.Description))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Description");
        if (!_validationService.IsValidStuffs(truck.Manufacturer))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Manufacturer");
        if (!_validationService.IsValidStuffs(truck.Model))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Model");
        if (!_validationService.IsValidStuffs(truck.SerialNumber))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid SerialNumber");
        return truck;
    }
}