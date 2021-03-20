using AutoMapper;
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
            CreateMap<DataObject, Aanbod>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GlobalId))
                .ForMember(dest => dest.GUID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Woonplaats))
                .ForPath(dest => dest.Makelaar.Id, opt => opt.MapFrom(src => src.MakelaarId))
                .ForPath(dest => dest.Makelaar.Name, opt => opt.MapFrom(src => src.MakelaarNaam))
                .ReverseMap();


            //CreateMap<DataObject, Makelaar>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Int32.Parse(src.MakelaarId)))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MakelaarNaam))
            //    .ReverseMap();
        }
    }
}
