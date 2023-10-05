using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using ListingType = Training.TruckWorld.Backend.Domain.Enums.ListingType;

namespace Training.TruckWorld.Backend.Application.Components.Models.Filters;

public class ComponentFilterDataModel
{
    /// <summary>
    /// Categories 
    /// </summary>
    public IEnumerable<KeyValuePair<string, ComponentCategory>> Categories { get; set; }

    /// <summary>
    /// Listing types (Action)
    /// </summary>
    public IEnumerable<KeyValuePair<string, ListingType>> ListingTypes { get; set; }

    /// <summary>
    /// Manufacturers (ishlab chiqaruvchilar)
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>> Manufacturers { get; set; }

    /// <summary>
    /// States (Davlat)
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>> States { get; set; }

    /// <summary>
    /// Conditions 
    /// </summary>
    public IEnumerable<KeyValuePair<string, ComponentCondition>> Conditions { get; set; }

    /// <summary>
    /// Contries (Mamlakatlar)
    /// </summary>
    public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }


    public ComponentFilterDataModel(IEnumerable<KeyValuePair<string, ComponentCategory>> categories, IEnumerable<KeyValuePair<string, ListingType>> listingTypes, IEnumerable<KeyValuePair<string, string>> manufacturers, IEnumerable<KeyValuePair<string, string>> states, IEnumerable<KeyValuePair<string, ComponentCondition>> conditions, IEnumerable<KeyValuePair<string, string>> countries)
    {
        Categories = categories;
        ListingTypes = listingTypes;
        Manufacturers = manufacturers;
        States = states;
        Conditions = conditions;
        Countries = countries;
    }
}
