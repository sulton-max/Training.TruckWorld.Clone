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

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default, 
        bool asNoTracking = false
        )
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(
        Guid userId, 
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return base.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default
        )
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(
        Guid userId, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return base.DeleteByIdAsync(userId , saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(
        User user, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default
        )
    {
        return base.DeleteAsync(user, saveChanges, cancellationToken);
    }
}
