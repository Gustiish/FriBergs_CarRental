using AutoMapper;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;

namespace FriBergs_CarRental.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarViewModel>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ReverseMap();
        }
    }
}
