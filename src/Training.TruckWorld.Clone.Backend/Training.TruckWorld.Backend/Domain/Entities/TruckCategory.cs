using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class TruckCategory : SoftDeletedEntity
{
    public string Name { get; set; }
    public TruckCategory() { }
    public TruckCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}

