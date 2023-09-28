using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Trucks.Services;

public interface ITruckValidationService
{
    /// <summary>
    /// string malumotlar uchun e.x: Manufacturer, Model, State, City
    /// </summary>
    bool IsValidStuff(string stuff);

    /// <summary>
    /// Category ni mavjudligi orqali tekshirish
    /// </summary>
    bool IsValidCategory(TruckCategory category);

    /// <summary>
    /// Description ni validligini tekshirish
    /// </summary>
    bool IsValidDescription(string description);

    /// <summary>
    /// Name (first and last name) ni tekshirish
    /// </summary>
    bool IsValidName(string name);

    /// <summary>
    /// Email tekshirish uchun
    /// </summary>
    bool IsValidEmailAddress(string email);

    /// <summary>
    /// phone numberni validligini tekshirish
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns
    bool IsValidPhoneNumber(string phoneNumber);
}
