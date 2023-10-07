using System.Linq.Expressions;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IUserCredentialsService
{
    IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate);
    
    ValueTask<ICollection<UserCredentials>> Get(IEnumerable<Guid> ids);
    
    ValueTask<UserCredentials?> GetByIdAsync(Guid id);
   
    ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<UserCredentials> UpdateAsync(string oldPassword, UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
