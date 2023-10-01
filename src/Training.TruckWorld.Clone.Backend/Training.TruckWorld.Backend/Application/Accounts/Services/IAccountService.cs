using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IAccountService
{
    User Register(RegisterDetails registerDetails);
    User Login(LoginDetails loginDetails);
}