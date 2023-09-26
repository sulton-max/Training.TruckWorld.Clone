using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Action = Training.TruckWorld.Backend.Domain.Enums.Action;

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
    public IEnumerable<KeyValuePair<string, Action>> ListingTypes { get; set; }

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

}
