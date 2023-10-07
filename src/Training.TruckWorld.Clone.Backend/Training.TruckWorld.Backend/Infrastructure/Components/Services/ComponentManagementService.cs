using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Infrastructure.Components.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services;

public class ComponentManagementService : IComponentManagementService
{
    private IComponentService _componentService;
    private IContactService _contactService;

    public ComponentManagementService(IComponentService componentService, IContactService contactService)
    {
        _componentService = componentService;
        _contactService = contactService;
    }

    public async ValueTask<ComponentDetails> CreateAsync(ComponentDetails componentDetails, Guid userId)
    {
        Validate(componentDetails);
        var component = componentDetails.Component;
        var contact = componentDetails.ContactDetailsId is null
            ? await _contactService.CreateAsync(componentDetails.ContactDetails)
            : _contactService.Get(contact => contact.Id == componentDetails.ContactDetailsId).FirstOrDefault();
        
        component.CategoryId = componentDetails.CategoryId;
        component.ContactId = contact.Id;
        component.UserId = userId;

        await _componentService.CreateAsync(component);
        return componentDetails;
    }

    private void Validate(ComponentDetails componentDetails)
    {
        if ((componentDetails.ContactDetailsId is null && componentDetails.ContactDetails is null)
            || (componentDetails.ContactDetailsId.HasValue && componentDetails.ContactDetails is not null))
            throw new InvalidEntityException(typeof(ComponentDetails), null, "Invalid  contact information!");


        if (componentDetails.ContactDetailsId != null && !_contactService.Get(contact => contact.Id == componentDetails.ContactDetailsId).Any())
            throw new EntityNotFoundException(typeof(ContactDetails));
    }
}