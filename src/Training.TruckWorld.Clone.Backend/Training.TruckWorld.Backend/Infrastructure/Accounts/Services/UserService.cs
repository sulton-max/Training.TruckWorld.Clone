using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class UserService : IUserService
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;

    public UserService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }

    public async ValueTask<User> CreateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ToValidate(user);

        await _appDataContext.Users.AddAsync(user, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return user;
    }

    public async ValueTask<User> DeleteAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundUser = await GetByIdAsync(user.Id, cancellationToken)
                        ?? throw new EntityNotFoundException(typeof(User));

        await _appDataContext.Users.RemoveAsync(foundUser, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return foundUser;
    }

    public async ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundUser = await GetByIdAsync(id, cancellationToken)
                        ?? throw new EntityNotFoundException(typeof(User));

        await _appDataContext.Users.RemoveAsync(foundUser, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return foundUser;
    }

    public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
        => _appDataContext.Users.Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<User>> GetAsync(IEnumerable<Guid> ids)
    {
        var users = _appDataContext.Users.Where(truck => ids.Contains(truck.Id));

        return new ValueTask<ICollection<User>>(users.ToList());
    }

    public ValueTask<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var foundUser = _appDataContext.Users.FirstOrDefault(user => user.Id == id);
        return new ValueTask<User?>(foundUser);
    }

    public async ValueTask<User> UpdateAsync(User user, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var foundUser = _appDataContext.Users.FirstOrDefault(searchingUser => searchingUser.Id == user.Id)
                        ?? throw new EntityNotFoundException(typeof(User));

        ToValidate(user);

        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;
        foundUser.EmailAddress = user.EmailAddress;

        await _appDataContext.Users.UpdateAsync(foundUser, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return foundUser;
    }

    private User ToValidate(User user)
    {
        if (!_validationService.IsValidFullName(user.FirstName) || !_validationService.IsValidFullName(user.LastName))
            throw new InvalidEntityException(typeof(User), user.Id, "Invalid Fullname");
        if (!_validationService.IsValidEmailAddress(user.EmailAddress))
            throw new InvalidEntityException(typeof(User), user.Id, "Invalid Email Address");
        return user;
    }
}