using SA_Project.Web.Models;

namespace SA_Project.Web.Service.IService
{
    public interface IAuthRestService
    {
        Task<LoginResponseDTO> Login(string url, LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(string url, RegisterRequestDTO registerRequestDTO);
    }
}
