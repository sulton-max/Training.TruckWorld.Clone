using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Products.Services;

public interface IFilterService
{
    /// <summary>
    /// Truck filter data modelini olish uchun method
    /// </summary>
    /// <returns></returns>
    TruckFilterDataModel GetTruckFilterDataModel();
    
    /// <summary>
    /// Trucklarni berilgan truck filter model malumotlari bilan filterlab , pagination qilib qaytarish uchun method
    /// </summary>
    /// <param name="filterModel"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageToken"></param>
    /// <returns></returns>
    ICollection<Truck> GetFilteredTrucks(TruckFilterModel filterModel, int pageSize = 20, int pageToken = 1);

    /// <summary>
    /// Component filter data modelini olish uchun
    /// </summary>
    /// <returns></returns>
    ComponentFilterDataModel GetComponentFilterDataModel();

    /// <summary>
    /// component filter model bo'yicha filterlangan componentlarni pagination bilan qaytarish uchun
    /// </summary>
    /// <param name="filterModel"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageToken"></param>
    /// <returns></returns>
    ICollection<Component> GetFilteredComponents(ComponentFilterModel filterModel, int pageSize = 20, int pageToken = 1);

    /// <summary>
    /// keyword bo'yicha filterlab pagination bilan qaytaradgan method
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    ICollection<IProduct> GetFilteredProducts(string keyword, int pageSize = 20, int pageToken = 1);
}
