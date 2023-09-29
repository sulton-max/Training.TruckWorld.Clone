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

        public async ValueTask<ICollection<Component>> GetFiltered(ComponentFilterModel filterModel, int pageSize = 20, int pageToken = 1)
        {

            var result = _dataContext.Components.Where(component =>
                filterModel.Manufacturers is null || filterModel.Manufacturers.Contains(component.Manufacturer)
                && filterModel.ListingTypes is null || filterModel.ListingTypes.Contains(component.Action)
                && filterModel.Categories is null || filterModel.Categories.Contains(component.Category)
                && filterModel.MinYear is null || filterModel.MinYear >= component.Year
                && filterModel.MaxYear is null || filterModel.MaxYear <= component.Year
                && filterModel.MinPrice is null || filterModel.MinPrice >= component.Price
                && filterModel.MaxPrice is null || filterModel.MaxPrice <= component.Price
                && filterModel.States is null || filterModel.States.Equals(component.Contact.Location.City)
                && filterModel.Conditions is null || filterModel.Conditions.Contains(component.Condition)
                && filterModel.Countries is null || filterModel.Countries.Contains(component.Contact.Location.Country)
                && filterModel.MinDate is null || filterModel.MinDate >= component.CreatedDate
                && filterModel.MaxDate is null || filterModel.MaxDate <= component.CreatedDate
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


