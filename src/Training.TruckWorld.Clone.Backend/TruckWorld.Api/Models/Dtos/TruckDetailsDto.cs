using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;

namespace TruckWorld.Api.Models.Dtos
{
    public class TruckDetailsDto
    {
        public TruckDto TruckDto { get; set; }
        public Guid? ContactId { get; set; }
        public Guid CategoryId { get; set; }
        public ContactDetailsDto? ContactDetailsDto { get; set; }
    }
}
