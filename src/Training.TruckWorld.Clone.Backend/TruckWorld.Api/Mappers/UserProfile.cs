using AutoMapper;

using TruckWorld.Api.Models.DTOs;
using TruckWorld.Domain.Entities;

namespace TruckWorld.Infrastructure.Common.MapperProfiles;

/// <summary>
/// AutoMapper profile for mapping between the User and UserDto classes.
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
