using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Action = Training.TruckWorld.Backend.Domain.Enums.Action;

namespace Training.TruckWorld.Backend.Application.Components.Models.Filters;

public class ComponentFilterModel
{
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
