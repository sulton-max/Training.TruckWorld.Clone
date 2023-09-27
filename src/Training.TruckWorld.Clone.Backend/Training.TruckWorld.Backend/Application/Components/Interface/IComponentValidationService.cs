namespace Training.TruckWorld.Backend.Application.Component.Interface;
public interface IComponentValidationService
{
    bool IsValidManufacturer(string manufacturer);
    bool IsValidModel(string model);
    bool IsValidSerialNumber(string serialNumber);
    bool IsValidYear(int year);
    bool IdValidQuantity(int quantity);
    bool IsValidWeight(int weight);
    bool IsValidDescription(string description);

}
