using Training.TruckWorld.Backend.Application.Accounts.Services;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
