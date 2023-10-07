using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Infrastructure.Trucks.Models;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Services;

public class TruckManagamentService : ITruckManagementService
{
    private ITruckService _truckService;
    private IContactService _contactService;
    public TruckManagamentService(ITruckService truckService, IContactService contactServcie)
    {
        _truckService = truckService;
        _contactService = contactServcie;
    }
    public async ValueTask<TruckDetails> CreateAsync(TruckDetails truckDetails, Guid userId)
    {
        ToValidate(truckDetails);
        var truck = truckDetails.Truck;
        var contact = truckDetails.ContactId is null ? await _contactService.CreateAsync(truckDetails.ContactDetails) : _contactService.Get(contact => contact.Id == truckDetails.ContactId).FirstOrDefault();

        truck.CategoryId = truckDetails.CategoryId;
        truck.ContactId = contact.Id;
        truck.UserId = userId;
        await _truckService.CreateAsync(truck);

        return truckDetails;
    }

    private async void ToValidate(TruckDetails truckDetails)
    {
        if ((!truckDetails.ContactId.HasValue && truckDetails.ContactDetails == null)
            || (truckDetails.ContactId.HasValue && truckDetails.ContactDetails != null))
            throw new InvalidEntityException(typeof(TruckDetails), null, "Invalid contact information!");
        if (truckDetails.ContactId != null && !_contactService.Get(contact => contact.Id == truckDetails.ContactId).Any())
            throw new EntityNotFoundException(typeof(ContactDetails));
        
    }
}
