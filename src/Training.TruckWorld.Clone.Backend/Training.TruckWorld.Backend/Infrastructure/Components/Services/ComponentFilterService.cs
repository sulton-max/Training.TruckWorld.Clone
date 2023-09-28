using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;


namespace Training.TruckWorld.Backend.Infrastructure.Components.Services
{
    public class ComponentFilterService : IComponentFilterService
    {
        private IDataContext _dataContext;
        
        public ComponentFilterService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async ValueTask<ICollection<Component>> Getfiltered(ComponentFilterModel filtermodel, int pageSize = 20, int pageToken = 1)
        {

            var result = _dataContext.Components.Where(component =>
                filtermodel.Manufacturers is null || filtermodel.Manufacturers.Contains(component.Manufacturer)
                && filtermodel.ListingTypes is null || filtermodel.ListingTypes.Contains(component.Action)
                && filtermodel.Categories is null || filtermodel.Categories.Contains(component.Category)
                && filtermodel.MinYear is null || filtermodel.MinYear >= component.Year
                && filtermodel.MaxYear is null || filtermodel.MaxYear <= component.Year
                && filtermodel.MinPrice is null || filtermodel.MinPrice >= component.Price
                && filtermodel.MaxPrice is null || filtermodel.MaxPrice <= component.Price
                && filtermodel.States is null || filtermodel.States.Equals(component.Contact.Location.City)
                && filtermodel.Conditions is null || filtermodel.Conditions.Contains(component.Condition)
                && filtermodel.Countries is null || filtermodel.Countries.Contains(component.Contact.Location.Country)
                && filtermodel.MinDate is null || filtermodel.MinDate >= component.CreatedDate
                && filtermodel.MaxDate is null || filtermodel.MaxDate <= component.CreatedDate
                ).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();

            return result; 
        }

        public async ValueTask<ICollection<Component>> SearchFiltered(string keyword, int pageSize = 20, int pageToken = 1)
        {
            var foundComponent = _dataContext.Components.Where(component =>
                component.Model.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                component.Manufacturer.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
            
            return foundComponent;
        }
    }
}


