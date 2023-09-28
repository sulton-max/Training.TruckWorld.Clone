using System.Net.Security;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Application.Configs;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;


//###Implement ComponentFilterService

//###Description
//ComponentFilterService should filter collection of Component model by given ComponentFilterModel

//Requirements
//-should get filtered collection by given filtermodel
//-can search components by given keyword
//-keyword is from component properties:
//manufacturer or model
//-service that returns IQueryable

//Deliverables
//service that filters components

namespace Training.TruckWorld.Backend.Infrastructure.Components.Services
{
    public class ComponentFilterService : IComponentFilterService
    {
        private List<Component> _components;

        public ComponentFilterService()
        {
            _components = new List<Component>();
        }

        public IQueryable<Component> GetFiltered(ComponentFilterModel filterModel) =>
            _components.Where(component => component.Manufacturer.ToLower() == 
            filterModel.Manufacturers.ToString().ToLower()).AsQueryable();

        public IQueryable<Component> SearchFiltered(string keyword)
             => _components.Where(found => found.Model.ToLower()
             .Contains(keyword.ToLower())).AsQueryable();
    }
}