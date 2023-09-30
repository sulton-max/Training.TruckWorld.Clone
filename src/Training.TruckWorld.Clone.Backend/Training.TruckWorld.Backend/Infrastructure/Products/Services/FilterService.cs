using System.ComponentModel;
using Microsoft.VisualBasic;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Products.Interfaces;
using Training.TruckWorld.Backend.Application.Products.Services;
using Training.TruckWorld.Backend.Application.Trucks.Models.Filters;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Component = Training.TruckWorld.Backend.Domain.Entities.Component;

namespace Training.TruckWorld.Backend.Infrastructure.Products.Services;

public class FilterService : IFilterService
{
    private IDataContext _appDataContext;

    public FilterService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public TruckFilterDataModel GetTruckFilterDataModel()
    {
        var truckFilterDataModel = new TruckFilterDataModel(
            _appDataContext.Trucks.Select(truck => truck.ListingType).Distinct().Select(listingType =>
            {
                return new KeyValuePair<string, ListingType>(
                    $"{listingType.ToString()} ({_appDataContext.Trucks.Count(truck => truck.ListingType == listingType)})",
                    listingType);
            }),
            _appDataContext.Trucks.Select(truck => truck.Category).Distinct().Select(category =>
            {
                return new KeyValuePair<string, TruckCategory>(
                    $"{category.ToString()} ({_appDataContext.Trucks.Count(truck => truck.Category == category)})",
                    category);
            }),
            _appDataContext.Trucks.Select(truck => truck.Manufacturer).Distinct().Select(manufacturer =>
            {
                return new KeyValuePair<string, string>(
                    $"{manufacturer} ({_appDataContext.Trucks.Count(truck => truck.Manufacturer == manufacturer)})",
                    manufacturer);
            }),
            _appDataContext.Trucks.Select(truck => truck.ContactUser.Location.City).Distinct().Select(state =>
            {
                return new KeyValuePair<string, string>(
                    $"{state} ({_appDataContext.Trucks.Count(truck => truck.ContactUser.Location.City == state)})",
                    state);
            }),
            _appDataContext.Trucks.Select(truck => truck.Condition).Distinct().Select(condition =>
            {
                return new KeyValuePair<string, TruckCondition>(
                    $"{condition.ToString()} ({_appDataContext.Trucks.Count(truck => truck.Condition == condition)})",
                    condition);
            }),
            _appDataContext.Trucks.Select(truck => truck.ContactUser.Location.Country).Distinct().Select(country =>
            {
                return new KeyValuePair<string, string>(
                    $"{country} ({_appDataContext.Trucks.Count(truck => truck.ContactUser.Location.Country == country)})",
                    country);
            })
        );

        return truckFilterDataModel;
    }

    public ComponentFilterDataModel GetComponentFilterDataModel()
    {
        var componentFilterDataModel = new ComponentFilterDataModel(
            _appDataContext.Components.Select(component => component.Category).Distinct().Select(category =>
            {
                return new KeyValuePair<string, ComponentCategory>(
                    $"{category.ToString()} ({_appDataContext.Components.Count(component => component.Category == category)})",
                    category);
            }),
            _appDataContext.Components.Select(component => component.ListingType).Distinct().Select(listingType =>
            {
                return new KeyValuePair<string, ListingType>(
                    $"{listingType} ({_appDataContext.Components.Count(component => component.ListingType == listingType)})",
                    listingType);
            }),
            _appDataContext.Components.Select(component => component.Manufacturer).Distinct().Select(manufacturer =>
            {
                return new KeyValuePair<string, string>(
                    $"{manufacturer} ({_appDataContext.Components.Count(component => component.Manufacturer == manufacturer)})",
                    manufacturer);
            }),
            _appDataContext.Components.Select(component => component.Contact.Location.City).Distinct().Select(state =>
            {
                return new KeyValuePair<string, string>(
                    $"{state} ({_appDataContext.Components.Count(component => component.Contact.Location.City == state)})",
                    state);
            }),
            _appDataContext.Components.Select(component => component.Condition).Distinct().Select(condition =>
            {
                return new KeyValuePair<string, ComponentCondition>(
                    $"{condition.ToString()} ({_appDataContext.Components.Count(component => component.Condition == condition)})",
                    condition);
            }),
            _appDataContext.Components.Select(component => component.Contact.Location.Country).Distinct().Select(
                country =>
                {
                    return new KeyValuePair<string, string>(
                        $"{country} ({_appDataContext.Components.Count(component => component.Contact.Location.Country == country)})",
                        country);
                })
        );

        return componentFilterDataModel;
    }

    public ICollection<Truck> GetFilteredTrucks(TruckFilterModel filterModel, int pageSize = 20, int pageToken = 1)
    {
        return _appDataContext.Trucks.Where(truck =>
            (filterModel.Keyword == null ||
             (truck.Manufacturer.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)
              || truck.Model.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)))
            && (filterModel.ListingTypes == null || filterModel.ListingTypes.Contains(truck.ListingType))
            && (filterModel.Categories == null || filterModel.Categories.Contains(truck.Category))
            && (filterModel.MinYear == null || filterModel.MinYear <= truck.Year)
            && (filterModel.MaxYear == null || filterModel.MaxYear >= truck.Year)
            && (filterModel.MinOdometer == null || filterModel.MinOdometer <= truck.Odometer)
            && (filterModel.MaxOdometer == null || filterModel.MaxOdometer <= truck.Odometer)
            && (filterModel.MinPrice == null || filterModel.MinPrice <= truck.Price)
            && (filterModel.MaxPrice == null || filterModel.MaxPrice >= truck.Price)
            && (filterModel.MinDate == null || filterModel.MinDate <= truck.CreatedDate)
            && (filterModel.MaxDate == null || filterModel.MaxDate >= truck.CreatedDate)
        ).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
    }

    public ICollection<Component> GetFilteredComponents(ComponentFilterModel filterModel, int pageSize = 20,
        int pageToken = 1)
    {
        return _appDataContext.Components.Where(component =>
            (filterModel.Keyword is null ||
             (component.Manufacturer.Contains(filterModel.Keyword, StringComparison.OrdinalIgnoreCase)
              || component.Model.Contains(filterModel.Keyword)))
            && (filterModel.ListingTypes == null || filterModel.ListingTypes.Contains(component.ListingType))
            && (filterModel.Categories == null || filterModel.Categories.Contains(component.Category))
            && (filterModel.MinYear == null || filterModel.MinYear <= component.Year)
            && (filterModel.MaxYear == null || filterModel.MaxYear >= component.Year)
            && (filterModel.MinDate == null || filterModel.MinDate <= component.CreatedDate)
            && (filterModel.MaxDate == null || filterModel.MaxDate >= component.CreatedDate)
            && (filterModel.MinPrice == null || filterModel.MinPrice <= component.Price)
            && (filterModel.MaxPrice == null || filterModel.MaxPrice >= component.Price)
        ).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
    }

    public ICollection<IProduct> GetFilteredProducts(string? keyword = null, int pageSize = 20, int pageToken = 1)
    {
        var trucks = _appDataContext.Trucks.Where(truck =>
            keyword is null || truck.Manufacturer.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                            || truck.Model.Contains(keyword, StringComparison.OrdinalIgnoreCase)).AsQueryable();

        var components = _appDataContext.Components.Where(component =>
            keyword is null || component.Manufacturer.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                            || component.Model.Contains(keyword, StringComparison.OrdinalIgnoreCase)).AsQueryable();

        var products = trucks.Concat<IProduct>(components).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();

        return products;
    }
}