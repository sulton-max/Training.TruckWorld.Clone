using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities;

public class TruckCategory : SoftDeletedEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TruckCategory() { }
    public TruckCategory(int id, string name)
    {
        Id = id;
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}

