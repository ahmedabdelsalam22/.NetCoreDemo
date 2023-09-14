using SA_Project.Models;
using SA_Project.Models.Dtos;

namespace SA_Project.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto dto);
        Task<UserDto> Register(RegisterRequestDto dto);
        Task<bool> AssignRole(string email , string role);
    }
}
