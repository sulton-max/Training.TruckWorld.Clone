using System.Linq.Expressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
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

    public async ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        ValidateOnCreate(userCredentials);

        userCredentials.Password = _passwordHasherService.Hash(userCredentials.Password);

        await _appDataContext.UserCredentials.AddAsync(userCredentials, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return userCredentials;
    }

    public ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(userCredentials.Id, saveChanges, cancellationToken);
    }

    public async ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var credentials = await GetByIdAsync(id) ?? throw new EntityNotFoundException(typeof(UserCredentials));

        if (credentials.IsDeleted)
            throw new EntityDeletedException(typeof(UserCredentials), credentials.Id);

        await _appDataContext.UserCredentials.RemoveAsync(credentials, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return credentials;
    }

    public IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate)
        => _appDataContext.UserCredentials.Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<UserCredentials>> Get(IEnumerable<Guid> ids)
        => new(_appDataContext.UserCredentials
            .Where(credentials => ids.Contains(credentials.Id)).ToList());

    public async ValueTask<UserCredentials> UpdateAsync(string oldPassword, UserCredentials userCredentials,
        bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOnUpdate(userCredentials);

        var credentials = await GetByIdAsync(userCredentials.Id)
                          ?? throw new EntityNotFoundException(typeof(UserCredentials));

        
        if (!_passwordHasherService.Verify(oldPassword, credentials.Password))
            throw new IncorrectPasswordException("Password incorrect!");

        credentials.Password = _passwordHasherService.Hash(userCredentials.Password);

        await _appDataContext.UserCredentials.UpdateAsync(credentials, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return credentials;
    }

    public async ValueTask<UserCredentials?> GetByIdAsync(Guid id) => await _appDataContext.UserCredentials.FindAsync(id);

    private bool ValidateOnCreate(UserCredentials userCredentials)
    {
        if (UserCredentialsExists(userCredentials.Id))
            throw new EntityConflictException(typeof(UserCredentials), nameof(userCredentials));

        if (_appDataContext.UserCredentials.Any(credentials => credentials.UserId == userCredentials.UserId))
            throw new EntityConflictException(typeof(UserCredentials), nameof(userCredentials));

        ValidatePassword(userCredentials);
        return true;
    }

    private bool ValidateOnUpdate(UserCredentials userCredentials)
    {
        if (!UserCredentialsExists(userCredentials.Id))
        {
            throw new EntityNotFoundException(typeof(UserCredentials), userCredentials.Id);
        }

        ValidatePassword(userCredentials);
        return true;
    }

    private bool UserCredentialsExists(Guid id)
        => _appDataContext.UserCredentials.Any(credentials => credentials.Id == id);

    private bool ValidatePassword(UserCredentials userCredentials)
    {
        if (userCredentials.Password.Length < 8)
            throw new InvalidEntityException(typeof(UserCredentials), userCredentials.Id, "Invalid Password");

        return true;
    }
}