using System.Text.RegularExpressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Extensions;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsValidEmail(string email) => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public bool IsValidName(string name)
        {
            if (!string.IsNullOrEmpty(name)
                                && name.Equals(name.ToCapitalized())
                                && name.All(letter => char.IsLetter(letter)))
                return true;
            return false;
        }

        public bool IsValid(string LastName)
        {
            return !string.IsNullOrEmpty(LastName)
                       && char.IsUpper(LastName[0])
                       && LastName.All(letter => char.IsLetter(letter));
        }

        public bool IsValidPassword(string password) => Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{8,}$");
        
    }
}
