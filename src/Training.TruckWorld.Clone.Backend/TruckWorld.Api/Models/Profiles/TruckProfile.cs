using AutoMapper;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Models.Profiles;

public class TruckProfile : Profile
{
    public TruckProfile()
    {
        CreateMap<TruckDetailsDto, Truck>();
        CreateMap<Truck, TruckDetailsDto>();
    }
}