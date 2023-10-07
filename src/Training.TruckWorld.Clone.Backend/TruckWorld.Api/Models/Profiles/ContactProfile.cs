using AutoMapper;
using Training.TruckWorld.Backend.Domain.Entities;
using TruckWorld.Api.Models.Dtos;

namespace TruckWorld.Api.Models.Profiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<ContactDetailsDto, ContactDetails>();
        CreateMap<ContactDetails, ContactDetailsDto>();
    }
}
