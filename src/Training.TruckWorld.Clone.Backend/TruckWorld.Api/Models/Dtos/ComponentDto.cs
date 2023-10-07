using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;

namespace TruckWorld.Api.Models.Dtos;

public class ComponentDto
{
    public Guid Id { get; set; }
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
    
    public ListingType ListingType { get; set; }
    
    public decimal Price { get; set; }
}