using System.Security.Cryptography;

using Training.TruckWorld.Backend.Application.Component.Interface;

namespace Training.TruckWorld.Backend.Infrastructure.Component.Services;

public class ComponentValidatinService : IComponentValidationService
{

    public bool IdValidQuantity(int quantity)
    {
       if(quantity > 0)
        {
            return true;
        }
       return false;
    }

    public bool IsValidDescription(string description)
    {
        if (description.Length <= 1000) 
        {
            return false;
        }
        return true;
    }

    public bool IsValidManufacturer(string manufacturer)
    {
        if(manufacturer.Length < 100 && string.IsNullOrWhiteSpace(manufacturer)) 
        {
            return false;
        }
        return true;
    }

    public bool IsValidModel(string model)
    {
        if (model.Length < 100 && string.IsNullOrWhiteSpace(model))
        {
            return false;
        }
        return true;
    }

    public bool IsValidSerialNumber(string serialNumber)
    {
        if(serialNumber.Length < 100 && string.IsNullOrWhiteSpace(serialNumber)) 
        {
            return false;
        }
        return true;
    }

    public bool IsValidWeight(int weight)
    {
        if(weight > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsValidYear(int year)
    {
        if(year > 1900 && year < DateTime.Now.Year)
        {
            return true;
        }
        return false;
    }
}
