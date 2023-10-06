using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IUserService
{
    IQueryable<User> Get(Expression<Func<User, bool>> predicate);

    ValueTask<ICollection<User>> GetAsync(IEnumerable<Guid> ids);
    
    ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
