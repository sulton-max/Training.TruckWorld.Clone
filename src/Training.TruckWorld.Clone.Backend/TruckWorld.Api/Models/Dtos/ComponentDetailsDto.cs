using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Models.Dtos;

public class ComponentDetailsDto
{
    public ComponentDto ComponentDto {get; set; }
    public Guid? ContactId { get; set; }
    public ContactDetailsDto? ContactDetailsDto { get; set; }
}