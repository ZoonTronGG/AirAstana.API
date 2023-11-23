using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.DAL;
using AutoMapper;

namespace AirAstana.API.Core.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Flight, FlightDto>().ReverseMap();
        CreateMap<Flight, FlightCreateDto>().ReverseMap();
        CreateMap<Flight, FlightChangeStatusDto>().ReverseMap();
    }
}