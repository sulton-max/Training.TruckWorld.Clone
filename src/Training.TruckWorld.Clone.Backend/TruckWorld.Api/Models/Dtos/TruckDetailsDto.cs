namespace TruckWorld.Api.Models.Dtos;

public class TruckDetailsDto
{
    public TruckDto TruckDto { get; set; }
    public Guid? ContactId { get; set; }
    public ContactDetailsDto? ContactDetailsDto { get; set; }
}