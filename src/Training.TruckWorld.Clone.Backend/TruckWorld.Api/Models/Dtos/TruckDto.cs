using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;



namespace TruckWorld.Api.Models.Dtos
{
    public class TruckDto
    {
        public Guid? Id { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public TruckCategory Category { get; set; }
        public int Year { get; set; }
        public TruckCondition Condition { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Odometer { get; set; }
        public ListingType ListingType { get; set; }
        public string? EngineType { get; set; }
        public string? FuelType { get; set; }
        public string? Color { get; set; }
        public TruckDto() { }
        public TruckDto(string serialNumber, string manufacturer, string model, TruckCategory category, int year, TruckCondition condition, string description, double price, double odometer, ListingType listingType, string? engineType, string? fuelType, string? color)
        {
            Id = Guid.NewGuid();
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
        }
    }
}
