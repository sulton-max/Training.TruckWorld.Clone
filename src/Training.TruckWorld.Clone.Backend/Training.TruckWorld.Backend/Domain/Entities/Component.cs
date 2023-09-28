using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Enums;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class Component : SoftDeletedEntity
{
    public Guid UserId { get; set; }
    public ComponentCategory Category { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public int Year { get; set; }
    public int Quantity { get; set; }
    public double Weight { get; set; }
    public ComponentCondition Condition { get; set; }
    public string Description { get; set; }
    public ListingType Action { get; set; }
    public decimal Price { get; set; }
    public ContactUser Contact { get; set; }
    
    public Component() { }

    public Component(Guid userId, ComponentCategory category, string manufacturer, string model,
        string serialNumber, int year, int quantity, double weight,
        ComponentCondition condition, string description, ContactUser contact, ListingType action)
    {
        Id = Guid.NewGuid();
        Category = category;
        Manufacturer = manufacturer;
        Model = model;
        SerialNumber = serialNumber;
        Year = year;
        Quantity = quantity;
        Weight = weight;
        Condition = condition;
        Description = description;
        Contact = contact;
        Action = action;
        CreatedDate = DateTime.UtcNow;
    }
}
