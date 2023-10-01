using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IAccountService
{
    ValueTask<User> Register(RegisterDetails registerDetails);
    ValueTask<User> Login(LoginDetails loginDetails);
}