using Training.TruckWorld.Backend.Domain.Enums;

namespace Training.TruckWorld.Backend.Application.Products.Interfaces;

public interface IProduct
{
    public Guid UserId { get; set; }
    
    public string SerialNumber { get; set; }

    string Manufacturer { get; set; }

    string Model { get; set; }
    
    public int Year { get; set; }
    
    public string Description { get; set; }
    
    public Guid ContactId { get; set; }
    
    public decimal Price { get; set; }
    
    public ListingType ListingType { get; set; }


}