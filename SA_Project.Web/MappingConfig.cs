using AutoMapper;
using SA_Project.Web.Models;

namespace SA_Project.Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Order,OrderDto>();
        }
    }
}
