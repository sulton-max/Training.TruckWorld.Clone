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

    public TruckService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }

    public async ValueTask<Truck> CreateAsync(Truck truck, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(truck);

        await _appDataContext.Trucks.AddAsync(truck, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

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
            await _appDataContext.SaveChangesAsync();
        
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
            _appDataContext.Trucks.Select(truck => truck.Category).Distinct().Select(category =>
            {
                return new KeyValuePair<string, TruckCategory>(
                    $"{category.ToString()} ({_appDataContext.Trucks.Count(truck => truck.Category == category)})",
                    category);
            }),
            _appDataContext.Trucks.Select(truck => truck.Manufacturer).Distinct().Select(manufacturer =>
            {
                return new KeyValuePair<string, string>(
                    $"{manufacturer} ({_appDataContext.Trucks.Count(truck => truck.Manufacturer == manufacturer)})",
                    manufacturer);
            }),
            _appDataContext.Trucks.Select(truck => truck.ContactUser.Location.City).Distinct().Select(state =>
            {
                return new KeyValuePair<string, string>(
                    $"{state} ({_appDataContext.Trucks.Count(truck => truck.ContactUser.Location.City == state)})",
                    state);
            }),
            _appDataContext.Trucks.Select(truck => truck.Condition).Distinct().Select(condition =>
            {
                return new KeyValuePair<string, TruckCondition>(
                    $"{condition.ToString()} ({_appDataContext.Trucks.Count(truck => truck.Condition == condition)})",
                    condition);
            }),
            _appDataContext.Trucks.Select(truck => truck.ContactUser.Location.Country).Distinct().Select(country =>
            {
                return new KeyValuePair<string, string>(
                    $"{country} ({_appDataContext.Trucks.Count(truck => truck.ContactUser.Location.Country == country)})",
                    country);
            })
        );

        return new ValueTask<TruckFilterDataModel>(dataModel);
    }


    public ValueTask<ICollection<Truck>> GetAsync(TruckFilterModel filterModel)
    {
        return new ValueTask<ICollection<Truck>>(_appDataContext.Trucks.Where(truck =>
            (filterModel.Keyword == null ||
             (truck.Manufacturer.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)
              || truck.Model.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)))
            && (filterModel.ListingTypes == null || filterModel.ListingTypes.Contains(truck.ListingType))
            && (filterModel.Categories == null || filterModel.Categories.Contains(truck.Category))
            && (filterModel.MinYear == null || filterModel.MinYear <= truck.Year)
            && (filterModel.MaxYear == null || filterModel.MaxYear >= truck.Year)
            && (filterModel.MinOdometer == null || filterModel.MinOdometer <= truck.Odometer)
            && (filterModel.MaxOdometer == null || filterModel.MaxOdometer <= truck.Odometer)
            && (filterModel.MinPrice == null || filterModel.MinPrice <= truck.Price)
            && (filterModel.MaxPrice == null || filterModel.MaxPrice >= truck.Price)
            && (filterModel.MinDate == null || filterModel.MinDate <= truck.CreatedDate)
            && (filterModel.MaxDate == null || filterModel.MaxDate >= truck.CreatedDate)
        ).Skip((filterModel.PageToken - 1) * filterModel.PageSize).Take(filterModel.PageSize).ToArray());
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
        foundTruck.Category = truck.Category;
        foundTruck.Year = truck.Year;
        foundTruck.Condition = truck.Condition;
        foundTruck.Description = truck.Description;
        foundTruck.Price = truck.Price;
        foundTruck.Odometer = truck.Odometer;
        foundTruck.ListingType = truck.ListingType;
        foundTruck.EngineType = truck.EngineType;
        foundTruck.FuelType = truck.FuelType;
        foundTruck.Color = truck.Color;
        foundTruck.ContactUser = truck.ContactUser;

        await _appDataContext.Trucks.UpdateAsync(foundTruck, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        
        return foundTruck;
    }

    private Truck ToValidate(Truck truck)
    {
        if (!_validationService.IsValidTruckCategory(truck.Category))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Category");
        if (!_validationService.IsValidDescription(truck.Description))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Description");
        if (!_validationService.IsValidEmailAddress(truck.ContactUser.EmailAddress))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid EmaildAddress");
        if (!_validationService.IsValidStuffs(truck.Manufacturer))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Manufacturer");
        if (!_validationService.IsValidStuffs(truck.Model))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid Model");
        if (!_validationService.IsValidStuffs(truck.SerialNumber))
            throw new InvalidEntityException(typeof(Truck), truck.Id, "Invalid SerialNumber");
        return truck;
    }
}