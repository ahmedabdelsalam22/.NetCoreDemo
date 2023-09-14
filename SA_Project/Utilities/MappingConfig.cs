using AutoMapper;
using SA_Project.Models;

namespace SA_Project.Utilities
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<RegisterRequestDto, ApplicationUser>();
        }
    }
}
