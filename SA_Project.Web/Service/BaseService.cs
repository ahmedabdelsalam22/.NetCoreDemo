using Newtonsoft.Json;
using SA_Project.Web.Models;
using SA_Project.Web.Utility;
using System.Text;
using System.Text.Json.Serialization;
using static SA_Project.Web.Utility.SD;

namespace SA_Project.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public APIResponse APIResponse { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            APIResponse = new APIResponse();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try 
            {
                var client = _httpClientFactory.CreateClient("SA_Project");

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

                httpRequestMessage.RequestUri = new Uri(SD.ApiUrl);
                httpRequestMessage.Headers.Add("Accept", "application/json");

                if (apiRequest.Data != null) 
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data) , 
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType) 
                {
                    case SD.ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;

                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                // response 

                HttpResponseMessage httpResponseMessage = null;

                httpResponseMessage = await client.SendAsync(httpRequestMessage);

                var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<T>(apiContent)!;
                return apiResponse;
            }
            catch(Exception ex)
            {
                var dto = new APIResponse()
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponse = JsonConvert.DeserializeObject<T>(res);
                return apiResponse;
            }
        }
    }
}
