using RestSharp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using SA_Project.Web.Service.IService;
using SA_Project.Web.Utility;
using SA_Project.Web.Models;

namespace Mango.Web.RestService
{
    public class AuthRestService : IAuthRestService
    {
        private readonly RestClient _restClient;

        public AuthRestService()
        {
            _restClient = new RestClient(SD.ApiUrl);
        }

        public async Task<LoginResponseDTO> Login(string url,LoginRequestDTO loginRequestDTO)
        {
            var request = new RestRequest(url, Method.Post);

            request.AddJsonBody(loginRequestDTO);

            request.AddHeader("Accept", "application/json");

           var response = await _restClient.ExecutePostAsync<LoginResponseDTO>(request);

            return response.Data!;
        }

        public async Task<UserDTO> Register(string url, RegisterRequestDTO registerRequestDTO)
        {
            var request = new RestRequest(url, Method.Post);

            request.AddJsonBody(registerRequestDTO);

            request.AddHeader("Accept", "application/json");

            var response = await _restClient.ExecutePostAsync<UserDTO>(request);

            return response.Data!;
        }
    }
}
