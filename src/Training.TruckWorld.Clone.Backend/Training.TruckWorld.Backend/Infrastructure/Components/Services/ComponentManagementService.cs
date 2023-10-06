using System.Runtime;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Infrastructure.Components.Models;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services;

public class ComponentManagementService : IComponentManagementService
{
    private IContactService _contactService;

    public ComponentManagementService(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async ValueTask<ComponentDetails> CreateAsync(ComponentDetails componentDetails)
    {
        Validate(componentDetails);

        var contact = componentDetails.ContactDetailsId is null
            ? await _contactService.CreateAsync(componentDetails.ContactDetails)
            : _contactService.Get(contact => contact.Id == componentDetails.ContactDetailsId).FirstOrDefault();

        var component = componentDetails.Component;
        component.ContactId = contact.Id;

        return componentDetails;
    }

    private void Validate(ComponentDetails componentDetails)
    {
        if ((componentDetails.ContactDetailsId is null && componentDetails.ContactDetails is null)
            || (componentDetails.ContactDetailsId.HasValue && componentDetails.ContactDetails is not null))
            throw new InvalidEntityException(typeof(ComponentDetails), null, "Invalid  contact information!");


        if (!_contactService.Get(contact => contact.Id == componentDetails.ContactDetailsId).Any())
        {
            throw new EntityNotFoundException(typeof(ContactDetails));
        }
    }
}