namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IPasswordHasherService
{
    /// <summary>
    /// For Password hasher
    /// </summary>
    /// <param name="password"></param>
    /// <returns>hash password</returns>
    string Hash(string password);
    
    /// <summary>
    /// Verify password 
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    bool Verify(string password, string hash);
}
