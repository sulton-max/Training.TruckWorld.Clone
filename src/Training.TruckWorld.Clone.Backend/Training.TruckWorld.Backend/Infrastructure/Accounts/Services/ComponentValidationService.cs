using System.Text.RegularExpressions;
using Training.TruckWorld.Backend.Application.Accounts.Services;

namespace Training.TruckWorld.Backend.Infrastructure.Accounts.Services;

public class ComponentValidationService : IComponentValidationService
{
    public bool CheckLocation(string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(city))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(street))
        {
            return false;
        }
        return true;
    }

    public bool CheckPhoneNumber(string phoneNumber)
    {
        string pattern = @"^(?:\+\d{1,4}\s?)?(?:\d{1,4}[-\s]?)(?:\d{1,4}[-\s]?)(?:\d{1,4})$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    public bool IsValidComponentItem(string item)
    {
        throw new NotImplementedException();
    }

    public bool IsValidManiforturer(string maniforturer) => true;
    public bool IsValidModel(string model) => true;
    public bool IsValidStockNumber(string stockNumber) => true;
}
