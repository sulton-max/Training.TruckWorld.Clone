using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class ContactService : IContactService
{
    private IDataContext _dataContext;
    private IValidationService _validationService;

    public ContactService(IDataContext dataContext, IValidationService validationService)
    {
        _dataContext = dataContext;
        _validationService = validationService;
    }

    public IQueryable<ContactDetails> Get(Expression<Func<ContactDetails, bool>> predicate)
        => _dataContext.Contacts.Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ContactDetails>> GetAsync(IEnumerable<Guid> ids)
    {
        return new ValueTask<ICollection<ContactDetails>>(_dataContext.Contacts
            .Where(contact => ids.Contains(contact.Id)).ToArray());
    }

    public async ValueTask<ContactDetails?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _dataContext.Contacts.FindAsync(id, cancellation)
           ?? throw new EntityNotFoundException(typeof(ContactDetails));

    public async ValueTask<ContactDetails> CreateAsync(ContactDetails contactDetails, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(contactDetails);

        var contact = await _dataContext.Contacts.AddAsync(contactDetails, cancellationToken);

        if (saveChanges)
            await _dataContext.Contacts.SaveChangesAsync(cancellationToken);

        return contact.Entity;
    }

    public async ValueTask<ContactDetails> UpdateAsync(ContactDetails contactDetails, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(contactDetails);

        var foundContact = await _dataContext.Contacts.FindAsync(contactDetails.Id, cancellationToken)
                           ?? throw new EntityNotFoundException(typeof(ContactDetails));

        foundContact.FirstName = contactDetails.FirstName;
        foundContact.LastName = contactDetails.LastName;
        foundContact.Email = contactDetails.Email;
        foundContact.PhoneNumber = contactDetails.PhoneNumber;
        foundContact.Country = contactDetails.Country;
        foundContact.City = contactDetails.City;

        await _dataContext.Contacts.UpdateAsync(foundContact, cancellationToken);

        if (saveChanges)
            await _dataContext.Contacts.SaveChangesAsync(cancellationToken);

        return foundContact;
    }

    public ValueTask<ContactDetails> DeleteAsync(ContactDetails contactDetails, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(contactDetails.Id, saveChanges, cancellationToken);
    }

    public async ValueTask<ContactDetails> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundContact = await _dataContext.Contacts.FindAsync(id, cancellationToken) ??
                           throw new EntityNotFoundException(typeof(ContactDetails));

        if (foundContact.IsDeleted)
            throw new EntityDeletedException(typeof(ContactDetails), foundContact.Id);

        await _dataContext.Contacts.RemoveAsync(foundContact);

        if (saveChanges)
            await _dataContext.Contacts.SaveChangesAsync(cancellationToken);

        return foundContact;
    }

    private void ToValidate(ContactDetails contactDetails)
    {
        if (!_validationService.IsValidFullName(contactDetails.FirstName) ||
            !_validationService.IsValidFullName(contactDetails.LastName))
        {
            throw new InvalidEntityException(typeof(ContactDetails), contactDetails.Id, "Invalid full name!");
        }

        if (!_validationService.IsValidEmailAddress(contactDetails.Email))
            throw new InvalidEntityException(typeof(ContactDetails), contactDetails.Id, "Invalid Email address!");

        if (!_validationService.IsValidPhoneNumber(contactDetails.PhoneNumber))
            throw new InvalidEntityException(typeof(ContactDetails), contactDetails.Id, "Invalid phone number!");

        if (!_validationService.IsValidStuffs(contactDetails.City)
            || !_validationService.IsValidStuffs(contactDetails.Country))
        {
            throw new InvalidEntityException(typeof(ContactDetails), contactDetails.Id, "Invalid location!");
        }
    }
}