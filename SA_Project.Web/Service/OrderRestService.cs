using RestCharpCourse.Services;
using SA_Project.Web.Models;
using SA_Project.Web.Service.IService;

namespace SA_Project.Web.Service
{
    public class OrderRestService : RestService<Order>, IOrderRestService
    {
        public OrderRestService(ITokenProvider tokenProvider) : base(tokenProvider)
        {
        }
    }
}
