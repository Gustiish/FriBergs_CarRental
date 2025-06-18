using AutoMapper;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;

namespace FriBergs_CarRental.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerOrders, opt => opt.MapFrom(src => src.CustomerOrders))
                .ReverseMap();
        }
    }
}
