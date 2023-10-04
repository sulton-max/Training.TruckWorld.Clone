using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Domain.Common;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;
#pragma warning disable CS8618


namespace Training.TruckWorld.Backend.Domain.Entities;

public class Truck: SoftDeletedEntity, IProduct
{
    public Guid UserId { get; set; }
    
    public string SerialNumber { get; set; }
    
    public string Manufacturer { get; set; }
    
    public string Model { get; set; }
    
    public TruckCategory Category { get; set; }
    
    public int Year { get; set; }
    
    public TruckCondition Condition { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Odometer { get; set; }
    
    public ListingType ListingType { get; set; }
    
    public string? EngineType { get; set; }
    
    public string? FuelType { get; set; }
    
    public string? Color { get; set; }
    
    public ContactUser ContactUser { get; set; }
    
    public Truck(){}
    public Truck(Guid userId, string serialNumber, string manufacturer, string model, TruckCategory category, int year, TruckCondition condition, string description, decimal price, decimal odometer, ListingType listingType, string? engineType, string? fuelType, string? color, ContactUser contactUser)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        SerialNumber = serialNumber;
        Manufacturer = manufacturer;
        Model = model;
        Category = category;
        Year = year;
        Condition = condition;
        Description = description;
        Price = price;
        Odometer = odometer;
        ListingType = listingType;
        EngineType = engineType;
        FuelType = fuelType;
        Color = color;
        ContactUser = contactUser;
        CreatedDate = DateTime.UtcNow;
    }
}
