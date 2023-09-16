using SA_Project.Web.Models;
using SA_Project.Web.Utility;
using static SA_Project.Web.Utility.SD;

namespace SA_Project.Web.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private IHttpClientFactory _httpClientFactory;
        public OrderService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<T> Create<T>(OrderCreateDto orderCreateDto)
        {
            return SendAsync<T>(new ApiRequest() 
            {
                ApiType = ApiType.POST,
                Url = SD.ApiUrl+ "api/Order/create",
                Data = orderCreateDto,
            });
        }

        public Task<T> Delete<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                Url = SD.ApiUrl + $"api/Order/delete/{id}",
            });
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = SD.ApiUrl + "api/Order/getAllOrders",
            });
        }

        public Task<T> GetById<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = SD.ApiUrl + $"api/Order/order/{id}",
            });
        }

    }
}
