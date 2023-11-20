using System.Linq.Expressions;

using TruckWorld.Domain.Entities;
using TruckWorld.Persistence.DataContext;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Persistence.Repositories;

public class UserRepository : EntityRepositoryBase<User, NotificationsDbContext>, IUserRepository
{
    public UserRepository(NotificationsDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges, 
        CancellationToken cancellationToken
        )
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(
        User user, 
        bool saveChanges, 
        CancellationToken cancellationToken
        )
    {
        return base.DeleteAsync(user, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate, 
        bool asNoTracking
        )
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(
        Guid userId, 
        bool asNoTracking,
        CancellationToken cancellationToken
        )
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking,
        CancellationToken cancellationToken
        )
    {
        return base.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges,
        CancellationToken cancellationToken
        )
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges, CancellationToken cancellationToken)
    {
        return base.DeleteByIdAsync(userId , saveChanges, cancellationToken);
    }
}
