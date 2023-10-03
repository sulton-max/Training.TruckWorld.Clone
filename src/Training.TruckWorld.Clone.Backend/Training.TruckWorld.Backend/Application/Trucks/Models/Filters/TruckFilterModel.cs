using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Filters.Models;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;

namespace Training.TruckWorld.Backend.Application.Trucks.Models.Filters;

public class TruckFilterModel : FilterPagination
{
    public string? Keyword { get; set; }
    
    public IEnumerable<ListingType>? ListingTypes { get; set; }
    
    public IEnumerable<TruckCategory>? Categories { get; set; }
    
    public IEnumerable<string>? Manufacturers { get; set; }
    
    public int? MinYear { get; set; }
    
    public int? MaxYear { get; set; }
    
    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }
    
    public decimal? MinOdometer { get; set; }
    
    public decimal? MaxOdometer { get; set; }
    
    public IEnumerable<string>? State { get; set; }
    
    public IEnumerable<TruckCondition> truckConditions { get; set; }
    
    public IEnumerable<string>? Country { get; set; }
    
    public DateTime? MinDate { get; set; }
    
    public DateTime? MaxDate { get; set; }
}
