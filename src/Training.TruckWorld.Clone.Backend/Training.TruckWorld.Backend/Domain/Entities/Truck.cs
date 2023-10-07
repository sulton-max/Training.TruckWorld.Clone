using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Enums;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;

#pragma warning disable CS8618


namespace Training.TruckWorld.Backend.Domain.Entities;

public class Truck : SoftDeletedEntity, IProduct
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
    
    public Guid ContactId { get; set; }
}