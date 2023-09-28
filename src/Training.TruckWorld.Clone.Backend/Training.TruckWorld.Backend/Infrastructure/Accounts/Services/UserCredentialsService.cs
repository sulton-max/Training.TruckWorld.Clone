using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Domain.Exceptions.UserCredentialsExceptions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class UserCredentialsService : IUserCredentialsService
{
    private IDataContext _appDataContext;
    private IPasswordHasherService _passwordHasherService;

    public UserCredentialsService(IDataContext appDataContext, IPasswordHasherService passwordHasherService)
    {
        _appDataContext = appDataContext;
        _passwordHasherService = passwordHasherService;

    }

    public async ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOnCreate(userCredentials);

        userCredentials.Password = _passwordHasherService.Hash(userCredentials.Password);

        await _appDataContext.UserCredentials.AddAsync(userCredentials);

        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();

        return userCredentials;
    }

    public ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(userCredentials.Id, saveChanges);
    }

    public async ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var credentials = await GetByIdAsync(id);

        if (credentials == null)
            throw new UserCredentialsNotFoundException("User credentials not found!");

        if (credentials.IsDeleted)
            throw new UserCredentialsAlreadyDeletedException("User credentials already deleted!");

        credentials.IsDeleted = true;
        credentials.DeletedDate = DateTime.UtcNow;

        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();

        return credentials;
    }

    public IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate)
        => _appDataContext.UserCredentials.Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<UserCredentials>> Get(IEnumerable<Guid> ids)
        => new ValueTask<ICollection<UserCredentials>>(_appDataContext.UserCredentials
            .Where(credentials => ids.Contains(credentials.Id)).ToList());

    public async ValueTask<UserCredentials> UpdateAsync(string oldPassword, UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOnUpdate(userCredentials);

        var credentials = await GetByIdAsync(userCredentials.Id);

        if (_passwordHasherService.Verify(oldPassword, credentials.Password))
            throw new IncorrectPasswordException("Password incorrect!");

        credentials.Password = _passwordHasherService.Hash(userCredentials.Password);
        credentials.ModifiedDate = DateTime.UtcNow;

        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();

        return credentials;
    }
    public async ValueTask<UserCredentials> GetByIdAsync(Guid id) => await _appDataContext.UserCredentials.FindAsync(id);


    private bool ValidateOnCreate(UserCredentials userCredentials)
    {
        if (UserCredentialsExists(userCredentials.Id))
            throw new UserCredentialsAlreadyExistsException("User credentials already exists !");

        if (_appDataContext.UserCredentials.Any(credentials => credentials.UserId == userCredentials.UserId))
            throw new UserAlreadyHasCredentialsException("The given user already has credentials!");

        ValidatePassword(userCredentials.Password);
        return true;
    }

    private bool ValidateOnUpdate(UserCredentials userCredentials)
    {
        if (!UserCredentialsExists(userCredentials.Id))
        {
            throw new UserCredentialsNotFoundException("user credentials not found!");
        }

        ValidatePassword(userCredentials.Password);
        return true;
    }

    private bool UserCredentialsExists(Guid id)
        => _appDataContext.UserCredentials.Any(credentials => credentials.Id == id);

    private bool ValidatePassword(string password)
    {
        if (password.Length < 8)
            throw new InvalidPasswordException("Password must contain at least 8 characters!");

        return true;
    }
}
