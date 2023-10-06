using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Infrastructure.Trucks.Models;
using Training.TruckWorld.Backend.Domain.Exceptions;
using AutoMapper.Internal.Mappers;
using System.Runtime.InteropServices;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Services;

public class TruckManagamentService : ITruckManagementService
{
    private ITruckService _truckService;
    private IContactService _contactService;
    public async ValueTask<TruckDetails> CreateAsync(TruckDetails truckDetails)
    {
        ToValidate(truckDetails);
        var truck = truckDetails.Truck;
        var contact = truckDetails.ContactId is null ? await _contactService.CreateAsync(truckDetails.ContactDetails) : _contactService.Get(contact => contact.Id == truckDetails.ContactId).FirstOrDefault();

        truck.ContactId = contact.Id;

        return truckDetails;
    }
    private async void ToValidate(TruckDetails truckDetails)
    {
        if ((!truckDetails.ContactId.HasValue && truckDetails.ContactDetails == null)
            || (truckDetails.ContactId.HasValue && truckDetails.ContactDetails != null))
            throw new InvalidEntityException(typeof(TruckDetails), null, "Invalid contact information!");
        
        if (!_contactService.Get(contact => contact.Id == truckDetails.ContactId).Any())
            throw new EntityNotFoundException(typeof(ContactDetails));
    }
}
