using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Extensions;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Trucks.Services;

public class TruckValidationService : ITruckValidationService
{

    private IDataContext _appDataContext;
    public TruckValidationService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public bool IsValidCategory(TruckCategory category) => _appDataContext.TruckCategories.Contains(category);

    public bool IsValidDescription(string description) => !string.IsNullOrWhiteSpace(description) && description.Length > 10;

    public bool IsValidEmailAddress(string email) => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    public bool IsValidName(string name)
    {
        //Is null or white spaces
        if (string.IsNullOrWhiteSpace(name)) return false;

        //Capitalized
        if (name.ToCapitalized() != name) return false;

        return true;
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return false;

        if (phoneNumber.Any(item => !Char.IsDigit(item))) return false;

        return true;
    }

    public bool IsValidStuff(string stuff) => !string.IsNullOrWhiteSpace(stuff);
}
