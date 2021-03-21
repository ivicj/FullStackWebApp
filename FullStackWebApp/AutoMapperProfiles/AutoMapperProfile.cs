using AutoMapper;
using FullStackWebApp.DTOs;
using FullStackWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackWebApp.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AanbodDTO, Aanbod>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GlobalId))
                .ForMember(dest => dest.GUID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Woonplaats))
                .ReverseMap();

            CreateMap<Aanbod, MakelaarDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MakelaarId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MakelaarNaam))
                .ForMember(dest => dest.AanbodCount, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
