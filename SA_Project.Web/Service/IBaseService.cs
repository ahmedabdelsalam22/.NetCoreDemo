using SA_Project.Web.Models;

namespace SA_Project.Web.Service
{
    public interface IBaseService
    {
        public APIResponse APIResponse { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
