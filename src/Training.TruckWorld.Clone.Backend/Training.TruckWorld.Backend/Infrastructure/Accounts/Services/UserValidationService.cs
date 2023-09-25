using System.Text.RegularExpressions;
using Training.TruckWorld.Backend.Application.Accounts.Serivces;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsValidEmailAddress(string emailAddress) => Regex.IsMatch(emailAddress, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        public bool IsValidName(string fullName) => !string.IsNullOrWhiteSpace(fullName);
        public bool IsValidPassword(string password) => Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$");

    }
}


