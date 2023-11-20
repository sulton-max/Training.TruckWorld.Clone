using System.Linq.Expressions;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Application.Common.Identity.Services;

/// <summary>
/// Service interface for managing user-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves a collection of users based on the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of users based on a collection of user IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a user based on application-specific criteria.
    /// </summary>
    /// <param name="user"></param>
    void IsValidUser(User user);
}
