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
        ValueTask<ICollection<Component>> GetFiltered(ComponentFilterModel filterModel, int pageSize = 20, int pageToken = 1);
        ValueTask<ICollection<Component>> SearchFiltered(string keyword, int pageSize = 20, int pageToken = 1);
    }
}
