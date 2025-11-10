using AutoMapper;
using KundenUmfrageTool.Api.Dtos;
using KundenUmfrageTool.Api.Models;

namespace KundenUmfrageTool.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role != null ? s.Role.Name : ""));
        }
    }
}

