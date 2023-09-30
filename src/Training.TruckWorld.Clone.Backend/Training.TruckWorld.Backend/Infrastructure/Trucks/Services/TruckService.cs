using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;

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
    public async ValueTask<Truck> CreateAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ToValidate(truck);
        _appDataContext.Trucks.AddAsync(truck, cancellationToken);
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return truck;
    }

    public async ValueTask<Truck> DeleteAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundTruck = await GetByIdAsync(truck.Id, cancellationToken);
        if (foundTruck != null)
            throw new InvalidOperationException("Truck not found");
        foundTruck.IsDeleted = true;
        foundTruck.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundTruck;
    }

    public async ValueTask<Truck> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundTruck = await GetByIdAsync(id, cancellationToken);
        if (foundTruck != null)
            throw new InvalidOperationException("Truck not found");
        foundTruck.IsDeleted = true;
        foundTruck.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundTruck;
    }

    public IQueryable<Truck> Get(Expression<Func<Truck, bool>> predicate)
    {
        return _appDataContext.Trucks.Where(predicate.Compile()).AsQueryable();
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

    public async ValueTask<Truck> UpdateAsync(Truck truck, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundTruck = _appDataContext.Trucks.FirstOrDefault(searchingTruck => searchingTruck.Id == truck.Id);

        if (foundTruck is null)
            throw new InvalidOperationException("Truck not found");
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
        foundTruck.ModifiedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return foundTruck;
    }
    private Truck ToValidate(Truck truck)
    {
        if (!_validationService.IsValidTruckCategory(truck.Category))
            throw new Exception("Invalid Category");
        if (!_validationService.IsValidDescription(truck.Description))
            throw new Exception("Invalid Description");
        if (!_validationService.IsValidEmailAddress(truck.ContactUser.EmailAddress))
            throw new Exception("Invalid EmaildAddress");
        if (!_validationService.IsValidStuffs(truck.Manufacturer))
            throw new Exception("Invalid Manufacturer");
        if (!_validationService.IsValidStuffs(truck.Model))
            throw new Exception("Invalid Model");
        if (!_validationService.IsValidStuffs(truck.SerialNumber))
            throw new Exception("Invalid SerialNumber");
        return truck;
    }

}
