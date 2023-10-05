using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Models;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IAccountService
{
    /// <summary>
    /// Register 
    /// </summary>
    /// <param name="registerDetails"></param>
    /// <returns></returns>
    ValueTask<User> Register(RegisterDetails registerDetails);
    
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="loginDetails"></param>
    /// <returns></returns>
    ValueTask<User> Login(LoginDetails loginDetails);
}