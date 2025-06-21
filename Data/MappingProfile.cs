using AutoMapper;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;

namespace FriBergs_CarRental.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ApplicationUser, ApplicationUserCreateViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CustomerOrders, opt => opt.MapFrom(src => src.CustomerOrders))
                .ReverseMap();


            CreateMap<ApplicationUser, ApplicationUserIndexViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CustomerOrders, opt => opt.MapFrom(src => src.CustomerOrders))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<CreateCustomerOrderViewModel, CustomerOrder>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ReverseMap();

        }


    }
}
