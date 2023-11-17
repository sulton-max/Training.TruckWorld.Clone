namespace TruckWorld.Api.Models.DTOs;

/// <summary>
/// Data transfer object (DTO) representing user information.
/// </summary>
public class UserDto
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}
