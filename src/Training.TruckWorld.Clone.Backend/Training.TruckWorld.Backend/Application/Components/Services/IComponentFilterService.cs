using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Application.Components.Models.Filters;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Application.Components.Services
{
    public interface IComponentFilterService
    {
        IQueryable<Component> GetFiltered(ComponentFilterModel filterModel);
        IQueryable<Component> SearchFiltered(string keyword);
    }
}
