using System.Text.RegularExpressions;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsValidEmail(string email) => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name)
                        && char.IsUpper(name[0])
                        && name.All(letter => char.IsLetter(letter));
        }

        public bool IsValidPassword(string password) => Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{8,}$");

    }
}
