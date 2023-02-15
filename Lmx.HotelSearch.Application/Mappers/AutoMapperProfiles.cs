using AutoMapper;
using GeoCoordinatePortable;
using Lmx.HotelSearch.Application.Commands;
using Lmx.HotelSearch.Application.Queries;
using Lmx.HotelSearch.Application.ViewModels;
using Lmx.HotelSearch.Domain;

namespace Lmx.HotelSearch.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeoLocation, GeoCoordinate>().ReverseMap();
            CreateMap<GeoLocation, SearchQuery>().ReverseMap();
            CreateMap<HotelViewModel, Hotel>().ReverseMap();
            CreateMap<SearchQuery, GeoCoordinate>().ReverseMap();

            CreateMap<UpdateHotelCommand, Hotel>()
                .ForMember(
                    dest => dest.GeoCoordinate,
                    opt => opt.MapFrom(src => src.GeoLocation)).ReverseMap();

            CreateMap<Hotel, HotelViewModel>()
                .ForMember(
                    dest => dest.GeoLocation,
                    opt => opt.MapFrom(src => src.GeoCoordinate)).ReverseMap();

            CreateMap<CreateHotelCommand, Hotel>()
                .ForMember(
                    dest => dest.GeoCoordinate,
                    opt => opt.MapFrom(src => src.GeoLocation)).ReverseMap();
        }
    }
}
