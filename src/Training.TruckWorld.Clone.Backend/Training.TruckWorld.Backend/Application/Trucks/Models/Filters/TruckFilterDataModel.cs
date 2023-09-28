using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;

namespace Training.TruckWorld.Backend.Application.Components.Models.Filters;

public class TruckFilterDataModel
{
    /// <summary>
    /// ListingTypes
    /// </summary>
    public IEnumerable<ListingType>? ListingTypes { get; set; }
    /// <summary>
    /// Categories 
    /// </summary>
    public IEnumerable<TruckCategory>? Categories { get; set; }
    /// <summary>
    /// Manufacturers (ishlab chiqarunchi - brand) 
    /// </summary>
    public IEnumerable<string>? Manufacturers { get; set; }
    /// <summary>
    /// State (davlat va shahar)
    /// </summary>
    public IEnumerable<string>? State { get; set; }
    /// <summary>
    /// Conditions
    /// </summary>
    public IEnumerable<TruckCondition> truckConditions { get; set; }
    /// <summary>
    /// Countries (mamlakatlar)
    /// </summary>
    public IEnumerable<string>? Country { get; set; }
}
