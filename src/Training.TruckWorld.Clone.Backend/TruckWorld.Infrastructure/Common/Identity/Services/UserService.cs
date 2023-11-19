using System.Linq.Expressions;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Options;

using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Entities;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Infrastructure.Common.Identity.Services;

public class UserService(IUserRepository userRepository, IOptions<ValidationSettings> validationSettings) : IUserService
{
    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        IsValidUser(user);

        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(
        Guid userId,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = null,
        bool asNoTracking = false
        )
    {
        return userRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
    {
        IsValidUser(user);

        return userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public void IsValidUser(User user)
    {
        if (!Regex.IsMatch(user.EmailAddress, validationSettings.Value.EmailRegexPattern))
            throw new InvalidOperationException("Invalid email address");

        if (!Regex.IsMatch(user.Password, validationSettings.Value.PasswordRegexPattern))
            throw new InvalidOperationException("Invalid password");
    }
}
