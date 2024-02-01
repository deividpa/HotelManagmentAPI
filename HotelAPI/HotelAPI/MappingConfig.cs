using AutoMapper;
using HotelAPI.Models;
using HotelAPI.Models.Dto;

namespace HotelAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

            CreateMap<Hotel, HotelCreateDto>();
            CreateMap<HotelCreateDto, Hotel>();

            CreateMap<Hotel, HotelUpdateDto>();
            CreateMap<HotelUpdateDto, Hotel>();
        }
    }
}
