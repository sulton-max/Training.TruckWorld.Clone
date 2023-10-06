using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IContactService
{
    IQueryable<ContactDetails> Get(Expression<Func<ContactDetails, bool>> predicate);

    ValueTask<ICollection<ContactDetails>> GetAsync(IEnumerable<Guid> ids);

    ValueTask<ContactDetails?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    ValueTask<ContactDetails> CreateAsync(ContactDetails contactDetails, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ContactDetails> UpdateAsync(ContactDetails contactDetails, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ContactDetails> DeleteAsync(ContactDetails contactDetails, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ContactDetails> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}