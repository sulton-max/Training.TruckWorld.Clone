using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Enums;
using Action = Training.TruckWorld.Backend.Domain.Enums.Action;

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
    public ContactUser Contact { get; set; }
    public Action Action { get; set; }

    public Component() { }

    public Component(ComponentCategory category, string manufacturer, string model,
        string serialNumber, int year, int quantity, double weight,
        ComponentCondition condition, string description, ContactUser contact, Action action)
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
