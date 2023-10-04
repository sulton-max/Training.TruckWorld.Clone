using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Application.Products.Services;
using Training.TruckWorld.Backend.Infrastructure.Products.Models.Filters;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Infrastructure.Products.Services;

public class FilterService : IFilterService
{
    private IDataContext _appDataContext;

    public FilterService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public ICollection<IProduct> GetFilteredProducts(ProductFilterModel productFilterModel)
    {
        var trucks = _appDataContext.Trucks.Where(truck =>
            productFilterModel.Keyword is null || truck.Manufacturer.Contains(productFilterModel.Keyword, StringComparison.OrdinalIgnoreCase)
                                              || truck.Model.Contains(productFilterModel.Keyword, StringComparison.OrdinalIgnoreCase)).AsQueryable();

        var components = _appDataContext.Components.Where(component =>
            productFilterModel.Keyword is null || component.Manufacturer.Contains(productFilterModel.Keyword, StringComparison.OrdinalIgnoreCase)
                                               || component.Model.Contains(productFilterModel.Keyword, StringComparison.OrdinalIgnoreCase)).AsQueryable();

        var products = trucks.Concat<IProduct>(components).Skip((productFilterModel.PageToken - 1) * productFilterModel.PageSize).Take(productFilterModel.PageSize).ToList();

        return products;
    }
}