using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.TruckWorld.Backend.Domain.Common;

namespace Training.TruckWorld.Backend.Domain.Entities
{
    public class Truck: SoftDeletedEntity
    {
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public TruckCategory Category { get; set; }
        public int Year { get; set; }
        public TruckCondition Condition { get; set; }
    }
}
