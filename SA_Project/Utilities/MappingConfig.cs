using AutoMapper;
using SA_Project.Models;
using SA_Project.Models.Dtos;

namespace SA_Project.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<RegisterRequestDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
