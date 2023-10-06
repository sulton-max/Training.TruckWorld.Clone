using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Enums;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;
#pragma warning disable CS8618

namespace Training.TruckWorld.Backend.Domain.Entities;

public class Component : SoftDeletedEntity, IProduct
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
    
    public ListingType ListingType { get; set; }
    
    public decimal Price { get; set; }
    
    public Guid ContactId { get; set; }
}