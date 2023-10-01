using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Exceptions;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class AccountService : IAccountService
{
    private IPasswordHasherService _passwordHasherService;
    private IUserService _userService;
    private IUserCredentialsService _credentialsService;

    public async ValueTask<User> Register(RegisterDetails registerDetails)
    {
        if (_userService.Get(user => user.EmailAddress == registerDetails.EmailAddress).Any())
            throw new ExistingEntityException(typeof(User));

        var user = new User(registerDetails.FirstName, registerDetails.LastName, registerDetails.EmailAddress);
        var credentials = new UserCredentials(user.Id, _passwordHasherService.Hash(registerDetails.Password));

        await _userService.CreateAsync(user);
        await _credentialsService.CreateAsync(credentials);
        return user;
    }

    public ValueTask<User> Login(LoginDetails loginDetails)
    {
        var foundUser = _userService.Get(user => user.EmailAddress == loginDetails.EmailAddress).FirstOrDefault();

        if (foundUser == null)
            throw new EntityNotFoundException(typeof(User));

        var userCredentials = _credentialsService.Get(x => x.UserId == foundUser.Id).FirstOrDefault();

        if (userCredentials == null)
        {
            throw new EntityNotFoundException(typeof(UserCredentials), foundUser.Id);
        }


        if (!_passwordHasherService.Verify(loginDetails.Password, userCredentials.Password))
            throw new IncorrectPasswordException("Password is incorrect!");

        return new ValueTask<User>(foundUser);
    }
}