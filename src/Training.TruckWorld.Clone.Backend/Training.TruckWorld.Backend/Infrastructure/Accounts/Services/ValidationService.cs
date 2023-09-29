using System.Linq;
using System.Text.RegularExpressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Extensions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class ValidationService : IValidationService
{
    private IDataContext _appDataContext;
    public ValidationService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }
    public bool IsValidComponentCategory(ComponentCategory category) => _appDataContext.ComponentsCategories.Contains(category);
    public bool IsValidDescription(string description) => !string.IsNullOrWhiteSpace(description) && description.Length > 10;
    public bool IsValidEmailAddress(string email) => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    public bool IsValidFullName(string name)
    {
        if (!string.IsNullOrEmpty(name)
                            && name.Equals(name.ToCapitalized())
                            && name.All(letter => char.IsLetter(letter)))
            return true;
        return false;
    }
    public bool IsValidName(string name) => !string.IsNullOrWhiteSpace(name) && name.Equals(name.ToCapitalized());
    public bool IsValidPhoneNumber(string phoneNumber) => !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Any(item => Char.IsDigit(item));
    public bool IsValidStuffs(string stuff) => !string.IsNullOrWhiteSpace(stuff);
    public bool IsValidTruckCategory(TruckCategory category) => _appDataContext.TruckCategories.Contains(category);
}
