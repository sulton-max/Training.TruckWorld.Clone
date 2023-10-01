using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;

namespace Training.TruckWorld.Backend.Application.Components.Models.Filters;

public class ComponentFilterModel
{
    public string? Keyword { get; set; }
    
    public IEnumerable<ComponentCategory>? Categories { get; set; }
    
    public IEnumerable<ListingType>? ListingTypes { get; set; }
    
    public IEnumerable<string>? Manufacturers { get; set; }
    
    public int? MinYear { get; set; }
    
    public int? MaxYear { get; set; }
    
    public decimal? MinPrice { get; set; }
    
    public decimal? MaxPrice { get; set; }
    
    public IEnumerable<string>? States { get; set; }
    
    public IEnumerable<ComponentCondition>? Conditions { get; set; }
    
    public IEnumerable<string>? Countries { get; set; }
    
    public DateTime? MinDate { get; set; }
    
    public DateTime? MaxDate { get; set; }

}
