using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Products.Models.Filters;

namespace Training.TruckWorld.Backend.Application.Products.Services;

public interface IFilterService
{
    /// <summary>
    /// keyword bo'yicha filterlab pagination bilan qaytaradgan method
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public ICollection<IProduct> GetFilteredProducts(ProductFilterModel productFilterModel);
}