

using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Options;

using TruckWorld.Application.Common.Identity.Services;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Domain.Entities;
using TruckWorld.Persistence.Repositories.Interface;

namespace TruckWorld.Infrastructure.Common.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ValidationSettings _validationSettings;
    public UserService(IUserRepository userRepository, IOptions<ValidationSettings> validationSettings)
    {
        _userRepository = userRepository;
        _validationSettings = validationSettings.Value;
    }

    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        )
        
    {
        IsValidUser(user);

        return _userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(
        Guid userId,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default
        )
    {
        return _userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = null,
        bool asNoTracking = false
        )
    {
        return _userRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default
        )
    {
        return _userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids, 
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
        )
    {
        return _userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default
        )
    {
        IsValidUser(user);

        return _userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public void IsValidUser(User user)
    {
        if (!Regex.IsMatch(user.EmailAddress, _validationSettings.EmailRegexPattern))
            throw new InvalidOperationException("Invalid email address");


        if (!Regex.IsMatch(user.Password, _validationSettings.PasswordRegexPattern))
            throw new InvalidOperationException("Invalid password");
    }
}
