using AutoMapper;
using TruckWorld.Api.Models.DTOs;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between the User and UserDto classes.
/// </summary>
public class UserMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserProfile
    /// </summary>
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
