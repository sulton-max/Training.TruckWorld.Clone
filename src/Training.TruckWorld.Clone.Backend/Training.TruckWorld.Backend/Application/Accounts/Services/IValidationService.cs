using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Accounts.Services;

public interface IValidationService
{
    bool IsValidFullName(string name);
    bool IsValidEmailAddress(string email);
    bool IsValidPhoneNumber(string phoneNumber);
    bool IsValidStuffs(string stuff);
    bool IsValidTruckCategory(TruckCategory category);
    bool IsValidComponentCategory(ComponentCategory category);
    bool IsValidDescription(string description);
    bool IsValidName(string name);
}
