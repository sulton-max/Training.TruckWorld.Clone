using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Models.Dtos;

public class SecurityActionDto
{
    public string OldPassword { get; set; }
    public UserCredentials UserCredentials { get; set; }
}
