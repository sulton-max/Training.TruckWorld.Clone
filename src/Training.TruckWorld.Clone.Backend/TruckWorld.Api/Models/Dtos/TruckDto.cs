using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;

namespace TruckWorld.Api.Models.Dtos;

public class TruckDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string SerialNumber { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public Guid CategoryId { get; set; }

    public int Year { get; set; }

    public TruckCondition Condition { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal Odometer { get; set; }

    public ListingType ListingType { get; set; }

    public string? EngineType { get; set; }

    public string? FuelType { get; set; }

    public string? Color { get; set; }
}

   