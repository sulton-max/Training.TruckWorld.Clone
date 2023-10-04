using AutoMapper;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Models.Profiles;

public class ComponentProfile : Profile
{
    public ComponentProfile()
    {
        CreateMap<Component, ComponentDto>();
        CreateMap<ComponentDto, Component>();
    }
}