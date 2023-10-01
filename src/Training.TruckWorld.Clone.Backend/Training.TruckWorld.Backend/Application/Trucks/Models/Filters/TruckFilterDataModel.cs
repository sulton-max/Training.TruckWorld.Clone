using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;

namespace Training.TruckWorld.Backend.Application.Trucks.Models.Filters;

public class TruckFilterDataModel
{
    /// <summary>
    /// ListingTypes
    /// </summary>
    public IEnumerable<KeyValuePair<string, ListingType>>? ListingTypes { get; set; }

    /// <summary>
    /// Categories 
    /// </summary>
    public IEnumerable<KeyValuePair<string, TruckCategory>>? Categories { get; set; }

    /// <summary>
    /// Manufacturers (ishlab chiqarunchi - brand) 
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>>? Manufacturers { get; set; }

    /// <summary>
    /// State (davlat va shahar)
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>>? State { get; set; }

    /// <summary>
    /// Conditions
    /// </summary>
    public IEnumerable<KeyValuePair<string, TruckCondition>>? Conditions { get; set; }

    /// <summary>
    /// Countries (mamlakatlar)
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>>? Country { get; set; }


    public TruckFilterDataModel(IEnumerable<KeyValuePair<string, ListingType>>? listingTypes,
        IEnumerable<KeyValuePair<string, TruckCategory>>? categories,
        IEnumerable<KeyValuePair<string, string>>? manufacturers, IEnumerable<KeyValuePair<string, string>>? state,
        IEnumerable<KeyValuePair<string, TruckCondition>>? conditions,
        IEnumerable<KeyValuePair<string, string>>? country)
    {
        ListingTypes = listingTypes;
        Categories = categories;
        Manufacturers = manufacturers;
        State = state;
        Conditions = conditions;
        Country = country;
    }
}