using AutoMapper;
using SA_Project.Models;
using SA_Project.Models.Dtos;
using SA_Project.Models.Order;

namespace SA_Project.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<RegisterRequestDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderCreateDto, Order>();
        }
    }
}
